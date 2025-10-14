using AnaliseCredito.Domain.Entities.Analises.Interfaces;
using AnaliseCredito.Domain.Entities.Analises.Service;
using AnaliseCredito.Domain.Entities.Base;
using AnaliseCredito.Domain.Entities.Clientes.Interfaces;
using AnaliseCredito.Domain.Entities.Clientes.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AnaliseCredito.Domain;

public static class DependencyInstallerDomain
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

        services.AddScoped<IAnaliseService, AnaliseService>();
        services.AddScoped<IClienteService, ClienteService>();
        return services;
    }
}