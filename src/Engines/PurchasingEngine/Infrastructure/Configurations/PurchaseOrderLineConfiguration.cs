using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.PurchasingEngine.Infrastructure.Configurations;

public class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
    {
        builder.ToTable("PurchaseOrderLines");

        builder.HasKey(pl => pl.Id);
        builder.Property(pl => pl.Id).HasConversion(id => id.Value, value => PurchaseOrderLineId.From(value));

        builder.Property(pl => pl.PurchaseOrderId).HasConversion(id => id.Value, value => PurchaseOrderId.From(value));
        builder.Property(pl => pl.ProductName).HasMaxLength(200).IsRequired();

        builder.Property(pl => pl.Quantity)
            .HasConversion(q => q.Value, v => Quantity.Create(v))
            .IsRequired();

        builder.OwnsOne(pl => pl.UnitPrice, money =>
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

        builder.OwnsOne(pl => pl.LineTotal, money =>
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
