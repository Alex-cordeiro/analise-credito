using System.Linq.Expressions;

namespace AnaliseCredito.Domain.Entities.Base
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private IBaseDomainRepository<T> _repository;

        public BaseService(IBaseDomainRepository<T> baseRepository)
        {
            _repository = baseRepository;
        }

        public async Task<bool> Create(T entity)
        {
            return await _repository.Add(entity);
        }

        public async Task<bool> Delete(T entity)
        {
            return await _repository.Delete(entity);
        }

        public IEnumerable<T?> GetAllAsync(bool isReadOnly = true, Expression<Func<T, bool>>? expression = null,
            string[]? includes = null)
        {
            return _repository.Get(isReadOnly, expression, includes);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Update(T entity)
        {
            return await _repository.Update(entity);
        }
    }
}
