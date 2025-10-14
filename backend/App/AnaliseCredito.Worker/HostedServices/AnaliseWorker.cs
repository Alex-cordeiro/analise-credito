using System.Text.Json;
using AnaliseCredito.Application.Analises.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitHole.Interfaces;

namespace AnaliseCredito.Worker.HostedServices;

public class AnaliseWorker : BackgroundService
{
    private readonly IRabbitConsumer _rabbitConsumer;
    private readonly string _queueName = "analise";
    private readonly IServiceScopeFactory  _serviceScopeFactory;
    

    public AnaliseWorker(IRabbitConsumer rabbitConsumer, IServiceScopeFactory serviceScopeFactory)
    {
        _rabbitConsumer = rabbitConsumer;
        _serviceScopeFactory = serviceScopeFactory;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = _rabbitConsumer.StartConsumingAsync(_queueName, async message =>
        {
            var messageProcessingResult = message;

            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true
            };
            
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var command = JsonSerializer.Deserialize<AnaliseProcessamentoCommand>(messageProcessingResult, options);
            await mediator.Send(command, stoppingToken);

        }, stoppingToken);
    }
}