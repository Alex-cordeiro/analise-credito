using AnaliseCredito.Worker.HostedServices;
using Microsoft.Extensions.DependencyInjection;
using RabbitHole.Extensions;

namespace AnaliseCredito.Worker;

public static class DependencyInstallerWorker
{
    public static IServiceCollection AddRabbitWorker(this IServiceCollection services, string host, int port,  string username, string password)
    {
        services.AddRabbitHole(x =>
        {
            x.HostName = host;
            x.Port = port;
            x.UserName = username;
            x.Password = password;
        });

        return services;
    }
    
    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<AnaliseWorker>();

        return services;
    }
}