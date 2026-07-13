using Eleraki.DeliveryEngine.Domain.Deliveries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.DeliveryEngine.Infrastructure.Configurations;

public class DeliveryLineConfiguration : IEntityTypeConfiguration<DeliveryLine>
{
    public void Configure(EntityTypeBuilder<DeliveryLine> builder)
    {
        builder.ToTable("DeliveryLines");

        builder.HasKey(dl => dl.Id);
        builder.Property(dl => dl.Id).HasColumnType("uniqueidentifier");

        builder.Property(dl => dl.DeliveryId).HasColumnType("uniqueidentifier");
        builder.Property(dl => dl.ProductDescription).HasMaxLength(500).IsRequired();

        builder.OwnsOne(dl => dl.Quantity, q =>
        {
            q.Property(qv => qv.Value)
                .HasColumnName("Quantity")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.OwnsOne(dl => dl.UnitPrice, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("UnitPriceCurrency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.OwnsOne(dl => dl.LineTotal, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("LineTotal")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("LineTotalCurrency")
                .HasMaxLength(3)
                .IsRequired();
        });
    }
}
