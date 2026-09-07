using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}