using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.PurchasingEngine.Infrastructure.Configurations;

public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.ToTable("PurchaseOrders");

        builder.HasKey(po => po.Id);
        builder.Property(po => po.Id).HasConversion(id => id.Value, value => PurchaseOrderId.From(value));

        builder.Property(po => po.VendorId).HasConversion(id => id.Value, value => VendorId.From(value));
        builder.Property(po => po.Status).HasConversion<int>();
        builder.Property(po => po.Notes).HasMaxLength(1000);

        builder.OwnsOne(po => po.TotalAmount, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("TotalAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("TotalAmountCurrency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.HasMany<PurchaseOrderLine>()
            .WithOne()
            .HasForeignKey(pl => pl.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
