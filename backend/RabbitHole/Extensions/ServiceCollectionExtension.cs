using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitHole.Interfaces;
using RabbitHole.Models;
using RabbitHole.Services;

namespace RabbitHole.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRabbitHole(this IServiceCollection services, Action<RabbitOptions> configure)
    {
        var options = new RabbitOptions();
        configure(options);

        services.AddSingleton(options);
        services.AddSingleton<IRabbitConnection, RabbitConnection>();
        services.AddSingleton<IRabbitPublisher, RabbitPublisher>();
        services.AddSingleton<IRabbitConsumer, RabbitConsumer>();

        return services;
    }
}
