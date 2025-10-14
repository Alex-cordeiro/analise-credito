using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using AnaliseCredito.Data.Contexts;

namespace AnaliseCredito.Api
{
    public class BaseContextFactory : IDesignTimeDbContextFactory<BaseContext>
    {
        public BaseContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile($"appsettings.{environmentName}.json")
               .Build();

            var connectionString = configuration.GetSection("ConnectionStrings:Default").Value;

            Console.WriteLine(connectionString);
            var optionsBuilder = new DbContextOptionsBuilder<BaseContext>();

            optionsBuilder.UseNpgsql(connectionString);

            return new BaseContext(optionsBuilder.Options);
        }
    }
}
