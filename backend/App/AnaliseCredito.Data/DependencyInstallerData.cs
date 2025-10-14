using AnaliseCredito.Data.Contexts;
using AnaliseCredito.Data.Repositories;
using AnaliseCredito.Data.UOW;
using AnaliseCredito.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace AnaliseCredito.Data
{
    public static class DependencyInstallerData
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BaseContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            //Generic
            services.AddScoped(typeof(IBaseDomainRepository<>), typeof(BaseRepository<>));
            
            //UnitOf Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}
