using System.Linq.Expressions;

namespace DaiPhuocBE.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> MaxAsync(Expression<Func<TEntity, int>> predicate);
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
