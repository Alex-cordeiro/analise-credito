using System.Linq.Expressions;

namespace AnaliseCredito.Domain.Entities.Base;

public interface IBaseDomainRepository<T>
{
    public Task<bool> Add(T entity);
    public Task<bool> Update(T entity);
    public Task<T?>  GetById(Guid id);
    public IEnumerable<T?> Get(bool isReadOnly = true, Expression<Func<T,bool>>? expression = null, string[]? includes = null);
    public Task<bool> Delete(T entity);
}