using MongoDB.Driver;

namespace MongoDBSample.Repository
{
    public interface IDbContext<TEntity>
    {
        IMongoCollection<TEntity> Entity { get; }
    }
}
