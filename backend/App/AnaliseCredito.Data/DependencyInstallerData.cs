using AnaliseCredito.Data.Contexts;
using AnaliseCredito.Data.Repositories;
using AnaliseCredito.Data.UOW;
using AnaliseCredito.Data.UOW.EF;
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
            services.AddTransient(typeof(IBaseDomainRepository<>), typeof(BaseRepository<>));
            
            //UnitOf Work
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}
