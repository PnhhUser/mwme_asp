using System;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IBaseRepo<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<int> SaveChangesAsync();
}
