using Diet.Application.Interface;
using DrMeet.Domain.Enums;
using DrMeet.Persistence.EF.Context;
using ErrorOr;
using Microsoft.EntityFrameworkCore.Storage;


namespace Diet.Persistence.EF.Repository;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly DrMeetDbContext _dbContext;

    public UnitOfWork(DrMeetDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        if (_transaction != null)
            return;

        _transaction = await _dbContext.Database.BeginTransactionAsync(ct);
    }

    public async Task<ErrorOr<TransactionStatus>> CommitAsync(CancellationToken ct = default)
    {
        try
        {
            await _dbContext.SaveChangesAsync();
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
            return TransactionStatus.Error;
        }

        return TransactionStatus.Success;
    }

    public async Task RollbackAsync(CancellationToken ct = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    public async Task<int> SaveAsync(CancellationToken ct = default)
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
    }
}