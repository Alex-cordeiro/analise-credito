using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AnaliseCredito.Data.Contexts
{
    public class BaseContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAcessor;

        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        public BaseContext(DbContextOptions<BaseContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAcessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(BaseContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
