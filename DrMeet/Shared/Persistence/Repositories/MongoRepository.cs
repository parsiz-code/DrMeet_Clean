using DrMeet.Api.Shared.Persistence.DbContexts.Mongo;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace DrMeet.Api.Shared.Persistence.Repositories;

public class MongoRepository<T>(IMongoDbContext context) : IRepository<T>
    where T : class
{
    private readonly IMongoCollection<T> _collection = context.GetCollection<T>();

    public IQueryable<T> AsQueryable() =>
        _collection.AsQueryable();

    public async Task<List<T>> GetAllAsync() =>
        await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
    public async Task<List<T>> GetFilterAsync(FilterDefinition<T> filter)=> 
        await _collection.Find(filter).ToListAsync();
      

    public async Task<T?> GetByIdAsync(string id) =>
        await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();

    public async Task AddAsync(T entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task AddRangeAsync(ICollection<T> entity)
        => await _collection.InsertManyAsync(entity);


    public async Task UpdateAsync(T entity)
    {
        var id = entity.GetType().GetProperty("Id")?.GetValue(entity)?.ToString();
        if (id is null) throw new Exception("No Id property");
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);
    }

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));

    public async Task DeleteAsync() =>
        await _collection.DeleteManyAsync(_ => true);

    public async Task<T> GetOrAddAsync(
        FilterDefinition<T> filter,
        Func<T> create)
    {
        var existing = await _collection.Find(filter).FirstOrDefaultAsync();
        if (existing is not null)
            return existing;

        var entity = create();
        await AddAsync(entity);
        return entity;
    }
}
