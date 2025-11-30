using Microsoft.EntityFrameworkCore;
using Data.Context;
using Domain.Interfaces;
using System.Linq.Expressions;

namespace Data.Repo
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected readonly MWMeDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepo(MWMeDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return true;
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }

}
