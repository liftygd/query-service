using Microsoft.EntityFrameworkCore;
using Domain.Abstract;

namespace Infrastructure.Repository;

public class Repository<TEntity>(
    BaseDbContext databaseContext)
    : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _entitySet = databaseContext.Set<TEntity>();
    
    public IQueryable<TEntity> Tracking => _entitySet.AsTracking();
    public IQueryable<TEntity> AsNoTracking => _entitySet.AsNoTracking();

    public async Task<TEntity?> GetById<TId>(TId id)
    {
        return await _entitySet.FindAsync(id);
    }

    public async Task<bool> InsertAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        
        await _entitySet.AddAsync(entity);
        await SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _entitySet.Update(entity);
        await SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        _entitySet.Remove(entity);
        await SaveChangesAsync();

        return true;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await databaseContext.SaveChangesAsync();
    }
}