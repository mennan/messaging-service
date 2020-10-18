using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MessagingService.Data;
using MessagingService.Entity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MessagingService.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoDbContext _mongoContext;
        private IMongoCollection<T> DbCollection;

        public Repository(IMongoDbContext context)
        {
            _mongoContext = context;
            DbCollection = _mongoContext.GetCollection<T>(typeof(T).Name);
        }

        public async Task Create(T obj)
        {
            if (obj == null) throw new ArgumentNullException(typeof(T).Name);
            
            DbCollection = _mongoContext.GetCollection<T>(typeof(T).Name);
            await DbCollection.InsertOneAsync(obj);
        }

        public async Task Update(T entity)
        {
            await DbCollection.ReplaceOneAsync(_ => _.Id == entity.Id, entity);
        }

        public async Task Delete(string id)
        {
            var objectId = new ObjectId(id);
            await DbCollection.DeleteOneAsync(_ => _.Id == id);
        }
        
        public async Task<T> GetOne(Expression<Func<T, bool>> predicate)
        {
            var result = await DbCollection.FindAsync<T>(predicate);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            var result = await DbCollection.FindAsync<T>(predicate);
            return await result.ToListAsync();
        }

        public async Task<List<T>> Get()
        {
            var result = await DbCollection.FindAsync(Builders<T>.Filter.Empty);
            return await result.ToListAsync();
        }
    }
}