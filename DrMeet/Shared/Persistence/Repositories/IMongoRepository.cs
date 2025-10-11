using MongoDB.Driver;

namespace DrMeet.Api.Shared.Persistence.Repositories;

/// <summary>
/// اینترفیس مخصوص عملیات MongoDB.
/// شامل متدهایی با استفاده از FilterDefinition.
/// </summary>
public interface IMongoRepository<T> : IRepository<T> where T : class
{
    Task<List<T>> GetFilterAsync(FilterDefinition<T> filter);
    Task<T?> GetByIdAsync(string id);
    Task DeleteAsync(string id);
    Task<T> GetOrAddAsync(FilterDefinition<T> filter, Func<T> create);
}
