using Eleraki.DeliveryEngine.Domain;
using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.Vehicles;
using Eleraki.DeliveryEngine.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.DeliveryEngine.Infrastructure.Persistence;

public class DeliveryDbContext : DbContext
{
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }

    public DbSet<Delivery> Deliveries => Set<Delivery>();
    public DbSet<DeliveryLine> DeliveryLines => Set<DeliveryLine>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeliveryLineConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryConfiguration());
        modelBuilder.ApplyConfiguration(new DriverConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
