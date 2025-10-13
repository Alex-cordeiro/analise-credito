using AnaliseCredito.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace AnaliseCredito.Data.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly BaseContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(BaseContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
            return; // já existe uma transação ativa

        _transaction = await _context.Database.BeginTransactionAsync();
    }
    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();

            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
    }
    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    public void Commit()
    {
        _context.SaveChanges();
    }
    public void BeginTransaction()
    {
        _context.Database.BeginTransactionAsync();
    }
    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
    public void Rollback()
    {
        _context.Database.RollbackTransaction();
    }

}
