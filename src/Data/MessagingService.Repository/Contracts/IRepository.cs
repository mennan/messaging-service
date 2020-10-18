using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MessagingService.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {       
        Task Create(TEntity obj);
        Task Update(TEntity obj);
        Task Delete(string id);
        Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> Get();
    }
}