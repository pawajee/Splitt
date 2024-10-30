using System.Linq.Expressions;

namespace Duc.Splitt.Core.Contracts.Repositories
{

    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> GetAsync(short id);
        Task<TEntity> GetAsync(byte id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        void AddAsync(TEntity entity);
        void AddRangeAsync(IEnumerable<TEntity> entities);

        //void Remove(TEntity entity);
        //void RemoveRange(IEnumerable<TEntity> entities);


    }
}
