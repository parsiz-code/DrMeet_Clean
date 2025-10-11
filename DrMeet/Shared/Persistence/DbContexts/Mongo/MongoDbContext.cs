using MongoDB.Driver;

namespace DrMeet.Api.Shared.Persistence.DbContexts.Mongo;

public class MongoDbContext(IMongoClient client, IConfiguration config) : IMongoDbContext
{
    private readonly IMongoDatabase _database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);

    public IMongoCollection<T> GetCollection<T>(string? name = null)
        => _database.GetCollection<T>(name ?? typeof(T).Name);
}