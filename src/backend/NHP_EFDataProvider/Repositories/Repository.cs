using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NHP_Domain.DataAdapters;
using NHP_EFDataProvider.Data;

namespace NHP_EFDataProvider.Repositories;

public abstract class Repository<T>(NhpDbContext context) : IRepository<T> where T : class
{
    protected readonly NhpDbContext Context = context;
    
    public async Task<T> CreateAsync(T entity)
    {
        var res = Context.Add(entity);
        await Context.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var res = Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
        return res.Entity;
    }

    public async Task DeleteAsync(T entity)
    {
        var res = Context.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<T?> FindAsync(T entity)
    {
        return await Context.Set<T>().FindAsync(entity);
    }

    public async Task<T?> FindAsync(params object[] keyValues)
    {
        return await Context.Set<T>().FindAsync(keyValues);
    }

    public virtual async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> condition)
    {
        return await Context.Set<T>().Where(condition).ToListAsync();
    }

    public virtual async Task<List<T>> FindAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public void Attach(T entity)
    {
        Context.Set<T>().Attach(entity);
    }

    public Task SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }
}