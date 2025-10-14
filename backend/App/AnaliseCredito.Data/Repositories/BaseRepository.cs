using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AnaliseCredito.Data.Contexts;
using AnaliseCredito.Domain.Entities.Base;
using Microsoft.Extensions.Logging;

namespace AnaliseCredito.Data.Repositories;

public class BaseRepository<T> : IBaseDomainRepository<T> where T : BaseEntity
{
    private BaseContext _context;
    private DbSet<T> _dbSet;
    private ILogger<BaseRepository<T>>  _logger;

    public BaseRepository(BaseContext context, ILogger<BaseRepository<T>> logger)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _logger = logger;
    }

    public async Task<bool> Add(T entity, bool saveChanges = true)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            
            if (saveChanges)
                await _context.SaveChangesAsync();

            return true;
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public IEnumerable<T?> Get(bool isReadOnly = true, Expression<Func<T, bool>>? expression = null,
        string[]? includes = null)
    {
        var query = isReadOnly ? _dbSet.AsNoTracking() : _dbSet;

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (expression != null)
            return query.Where(expression).AsQueryable();

        return query.AsQueryable();
    }

    public Task<bool> Delete(T entity, bool saveChanges = true)
    {
        try
        {
            _dbSet.Remove(entity);

            if (saveChanges)
                _context.SaveChanges();
            
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public Task<T?> GetById(Guid id)
    {
        return _dbSet.FirstOrDefaultAsync<T>(x => x.Id.Equals(id));
    }

    public Task<bool> Update(T entity, bool  saveChanges = true)
    {
        try
        {
            _dbSet.Update(entity);
            
            if (saveChanges)
                _context.SaveChanges();
            
            return Task.FromResult(true);
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            return Task.FromResult(false);
        }
    }
}