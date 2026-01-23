using DaiPhuocBE.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DaiPhuocBE.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly MasterDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(MasterDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>(); 
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public void DeleteAsync(TEntity entity)
        {
           _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<int> MaxAsync(Expression<Func<TEntity, int>> predicate)
        {
            // Kiểm tra xem table có rỗng hay không
            if (!await _context.Set<TEntity>().AnyAsync())
            {
                return 0;
            }
            return await _dbSet.MaxAsync(predicate);
        }

        public void UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
