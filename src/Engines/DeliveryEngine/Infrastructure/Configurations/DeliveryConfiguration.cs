using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.ValueObjects;
using Eleraki.DeliveryEngine.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.DeliveryEngine.Infrastructure.Configurations;

public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("Deliveries");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnType("uniqueidentifier");

        builder.Property(d => d.TrackingNumber)
            .HasConversion(tn => tn.Value, v => TrackingNumber.From(v))
            .HasMaxLength(50);

        builder.Property(d => d.RecipientName).HasMaxLength(200);
        builder.Property(d => d.DeliveryAddress).HasMaxLength(500);
        builder.Property(d => d.Status).HasConversion<int>();

        builder.Property(d => d.DriverId)
            .HasConversion(id => id!.Value, value => DriverId.From(value));

        builder.Property(d => d.VehicleId)
            .HasConversion(id => id!.Value, value => VehicleId.From(value));

        builder.Property(d => d.TotalAmount)
            .HasConversion(m => m.Amount, v => Money.Create(v, "USD"))
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(d => d.TotalAmountCurrency)
            .HasMaxLength(3)
            .IsRequired();

        builder.HasMany<DeliveryLine>()
            .WithOne()
            .HasForeignKey(dl => dl.DeliveryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
