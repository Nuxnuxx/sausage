using System.Linq.Expressions;

namespace NHP_Domain.DataAdapters;

public interface IRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task <T?> FindAsync(T entity);
    Task <T?> FindAsync(params object[] keyValues);
    Task <List<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> FindAllAsync();
    void Attach(T entity);
    Task SaveChangesAsync();
}