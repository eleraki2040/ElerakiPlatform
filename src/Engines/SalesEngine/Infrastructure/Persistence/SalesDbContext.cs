using Eleraki.SalesEngine.Domain.Customers;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SalesEngine.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.SalesEngine.Infrastructure.Persistence;

public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
    {
    }

    public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
    public DbSet<SalesOrderLine> SalesOrderLines => Set<SalesOrderLine>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SalesOrderConfiguration());
        modelBuilder.ApplyConfiguration(new SalesOrderLineConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
