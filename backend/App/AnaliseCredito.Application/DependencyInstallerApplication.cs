using System.Reflection;
using AnaliseCredito.Application.AppServices;
using AnaliseCredito.Application.Interfaces;
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

    public static IServiceCollection AddMediatRDependence(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(ctg => ctg.RegisterServicesFromAssembly(assembly));
        return services;
    }

    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IAnaliseAppService, AnaliseAppService>();
        services.AddScoped<IValidadorPropostaAppService, ValidadorPropostaAppService>();
        return services;
    }
}