using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.DeliveryEngine.Infrastructure.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasConversion(id => id.Value, value => DriverId.From(value));

        builder.Property(d => d.FullName).HasMaxLength(200);
        builder.Property(d => d.LicenseNumber).HasMaxLength(50);

        builder.Property(d => d.Phone)
            .HasConversion(p => p.Value, v => PhoneNumber.Create(v))
            .HasMaxLength(20);

        builder.Property(d => d.Email)
            .HasConversion(e => e.Value, v => Email.Create(v))
            .HasMaxLength(200);

        builder.Property(d => d.Status).HasConversion<int>();
    }
}
