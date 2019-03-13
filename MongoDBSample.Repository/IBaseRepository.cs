using MongoDBSample.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBSample.Repository
{
    public interface IBaseRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(string id);
        Task SaveAsync(TEntity item);
        Task<bool> UpdateAsync(TEntity item);
        Task<bool> DeleteAsync(TEntity item);
        Task<bool> DeleteAllAsync();
    }
}
