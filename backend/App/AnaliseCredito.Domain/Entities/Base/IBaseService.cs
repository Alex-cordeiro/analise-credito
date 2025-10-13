using System.Linq.Expressions;

namespace AnaliseCredito.Domain.Entities.Base
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<bool> Create(T entity);
        Task<bool> Delete(T entity);
        IEnumerable<T?> GetAllAsync(bool isReadOnly = true, Expression<Func<T,bool>>? expression = null, string[]? includes = null);
        Task<T?> GetByIdAsync(Guid id);
        Task<bool> Update(T entity);
    }
}
