using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.PurchasingEngine.Infrastructure.Persistence;

public class PurchasingDbContext : DbContext
{
    public PurchasingDbContext(DbContextOptions<PurchasingDbContext> options) : base(options) { }

    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderLine> PurchaseOrderLines => Set<PurchaseOrderLine>();
    public DbSet<Vendor> Vendors => Set<Vendor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseOrderLineConfiguration());
        modelBuilder.ApplyConfiguration(new VendorConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
