using Eleraki.DeliveryEngine.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.DeliveryEngine.Infrastructure.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasConversion(id => id.Value, value => VehicleId.From(value));

        builder.Property(v => v.LicensePlate).HasMaxLength(20);
        builder.Property(v => v.Model).HasMaxLength(100);
        builder.Property(v => v.Status).HasConversion<int>();
    }
}
