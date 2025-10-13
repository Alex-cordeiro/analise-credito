using AnaliseCredito.Domain.Entities.Base;
using Microsoft.Extensions.DependencyInjection;

namespace AnaliseCredito.Domain;

public static class DependencyInstallerDomain
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
        
        
        return services;
    }
}