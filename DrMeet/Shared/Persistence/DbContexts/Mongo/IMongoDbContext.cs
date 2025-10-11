using MongoDB.Driver;

namespace DrMeet.Api.Shared.Persistence.DbContexts.Mongo;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string? name = null);
}
