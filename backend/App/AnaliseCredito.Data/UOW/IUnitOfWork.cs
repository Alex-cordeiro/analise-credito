namespace AnaliseCredito.Data.UOW;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    Task CommitAsync();
    public Task BeginTransactionAsync();
    void Rollback();
    Task RollbackAsync();
}
