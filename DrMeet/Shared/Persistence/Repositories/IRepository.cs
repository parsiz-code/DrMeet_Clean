namespace DrMeet.Api.Shared.Persistence.Repositories;

/// <summary>
/// اینترفیس پایه برای عملیات عمومی روی داده‌ها.
/// شامل متدهای مشترک بین MongoDB و EF Core.
/// </summary>
public interface IRepository<T> where T : class
{
    IQueryable<T> AsQueryable();
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task AddRangeAsync(ICollection<T> entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync();
}
