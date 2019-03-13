using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBSample.Domain;
using MongoDBSample.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBSample.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity, string> where TEntity : IEntity
    {
        private readonly DbContext<TEntity> context = null;

        public BaseRepository(IOptions<DbConfiguration> configuration)
        {
            this.context = new DbContext<TEntity>(configuration);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await this.context.Entity.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // manage the exception
                throw ex;
            }
        }
        public async Task<TEntity> GetAsync(string id)
        {
            try
            {
                return await this.context.Entity
                                         .Find(_ => _.Id == id)
                                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // manage the exception
                throw ex;
            }
        }
        public async Task SaveAsync(TEntity item)
        {
            try
            {
                await this.context.Entity.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // manage the exception
                throw ex;
            }
        }
        public async Task<bool> UpdateAsync(TEntity item)
        {
            try
            {
                var actionResult = await this.context.Entity
                                                     .ReplaceOneAsync(_ => _.Id == item.Id
                                            , item
                                            , new UpdateOptions { IsUpsert = true });

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // manage the exception
                throw ex;
            }
        }
        public async Task<bool> DeleteAsync(TEntity item)
        {
            try
            {
                var actionResult = await this.context.Entity
                                                     .DeleteOneAsync(_ => _.Id == item.Id);

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // manage the exception
                throw ex;
            }
        }
        public async Task<bool> DeleteAllAsync()
        {
            try
            {
                var actionResult = await this.context.Entity.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // manage the exception
                throw ex;
            }
        }
    }
}
