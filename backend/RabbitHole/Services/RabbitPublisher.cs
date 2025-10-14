using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitHole.Interfaces;
using RabbitHole.Models;
using RabbitMQ.Client;

namespace RabbitHole.Services;

public class RabbitPublisher : IRabbitPublisher
{
    private readonly IRabbitConnection _connection;
    private readonly RabbitOptions _options;
    private readonly ILogger<RabbitPublisher> _logger;


    public RabbitPublisher(IRabbitConnection connection, RabbitOptions options, ILogger<RabbitPublisher> logger)
    {
        _connection = connection;
        _options = options;
        _logger = logger;
    }

    public async Task PublishAsync(string queueName, object message)
    {
        var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(
                     _options.RetryCount,
                    _ => TimeSpan.FromMilliseconds(_options.RetryDelayMilliseconds),
                        (ex, ts) => _logger.LogInformation("Retentando envio", _options.HostName)
                    );

        await policy.ExecuteAsync(async () =>
        {
            var connection = await _connection.GetConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var props = new BasicProperties { Persistent = true };

            await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queueName,
            mandatory: false,
            basicProperties: props,
            body: body
        );
        });
    }
}
