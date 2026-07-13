using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.InventoryEngine.Infrastructure.Persistence;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InventoryItemConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
