using LyMarket.Data;
using Microsoft.EntityFrameworkCore;

namespace LyMarket.Repositories;

public class GenericRepository<T>(
    LyMarketDbContext context,
    ILogger logger) : IGenericRepository<T>
    where T : class
{


    private readonly DbSet<T> _dbSet = context.Set<T>();
    protected readonly ILogger Logger = logger;

    public IQueryable<T> GetQueryAble() => _dbSet.AsQueryable();

    public virtual async Task<IEnumerable<T>> All() => await _dbSet.ToListAsync();


    public virtual async Task<T?> GetById(int id) => await _dbSet.FindAsync(id);

    public virtual async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public virtual Task<bool> Update(T entity)
    {
        _dbSet.Update(entity);
        return Task.FromResult(true);
    }

    public virtual Task<bool> Delete(T entity)
    {
        _dbSet.Remove(entity);
        return Task.FromResult(true);
    }
}
