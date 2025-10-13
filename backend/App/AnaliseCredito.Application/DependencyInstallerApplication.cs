using AnaliseCredito.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RabbitHole.Extensions;

namespace AnaliseCredito.Application;

public static class DependencyInstallerApplication
{

    public static IServiceCollection AddRabbitIntegration(this IServiceCollection services, string host, int port,  string username, string password)
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

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AnaliseCreateCommandValidator>();
        return services;
    }
}