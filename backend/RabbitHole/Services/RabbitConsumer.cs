using System.Text;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitHole.Interfaces;
using RabbitHole.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitHole.Services;

public class RabbitConsumer : IRabbitConsumer
{
    private readonly IRabbitConnection _connection;
    private readonly RabbitOptions _options;

    private readonly ILogger<RabbitConsumer> _logger;

    public RabbitConsumer(IRabbitConnection connection, RabbitOptions options)
    {
        _connection = connection;
        _options = options;
    }

    public async Task StartConsumingAsync(string queueName, Func<string, Task> onMessageReceived, CancellationToken token = default)
    {
        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: _options.RetryCount,
            sleepDurationProvider: _ => TimeSpan.FromMilliseconds(_options.RetryDelayMilliseconds),
            onRetry: (ex, ts) => _logger.LogInformation($"Falha no consumer, retentando consumir fila em {ts.TotalSeconds}", ex.Message)
        );

        await policy.ExecuteAsync(async () =>
        {
            var connection = await _connection.GetConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);


            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    await onMessageReceived(message);

                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: token);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Falha no consumo de mensagens");
                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
                    throw;
                }
            };

            var consumerTag = await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);


            _logger.LogInformation($"Consumidor iniciado na fila {queueName}, Tag: {consumerTag}");

            try
            {
                await Task.Delay(Timeout.Infinite, token);
            }
            catch (TaskCanceledException)
            {
                await channel.BasicCancelAsync(consumerTag);
                throw;
            }
        });
    }
}
