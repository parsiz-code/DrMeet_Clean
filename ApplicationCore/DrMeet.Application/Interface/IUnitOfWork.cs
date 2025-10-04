using DrMeet.Domain.Enums;
using DrMeet.Framework.Core.Interface;
using ErrorOr;

namespace Diet.Application.Interface;

public interface IUnitOfWork : IDisposable,IService
{
    Task<int> SaveAsync(CancellationToken ct = default);
    Task BeginTransactionAsync(CancellationToken ct = default);
    Task<ErrorOr<TransactionStatus>> CommitAsync(CancellationToken ct = default);
    Task RollbackAsync(CancellationToken ct = default);
}
