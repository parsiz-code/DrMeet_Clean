using System.Linq.Expressions;

namespace DrMeet.Api.Shared.Persistence.Repositories;

/// <summary>
/// اینترفیس مخصوص عملیات EF Core.
/// شامل متدهایی با استفاده از Expression.
/// </summary>
public interface IEFCoreRepository<T> where T : class
{
    IQueryable<T> AsQueryable();
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task AddRangeAsync(ICollection<T> entity);
    Task UpdateAsync(T entity);
    Task AttachAsync(T entity);
    Task DeleteAsync();
    Task DeleteRangeAsync(ICollection<T> entities);
    Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task<T> GetOrAddAsync(Expression<Func<T, bool>> predicate, Func<T> create);
}
