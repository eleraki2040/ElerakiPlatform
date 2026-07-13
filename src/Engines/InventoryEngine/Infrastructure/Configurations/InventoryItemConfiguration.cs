using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.InventoryEngine.Infrastructure.Configurations;

public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("InventoryItems");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasConversion(id => id.Value, value => InventoryItemId.From(value));

        builder.Property(i => i.Sku)
            .HasConversion(sku => sku.Value, value => Sku.Create(value))
            .HasMaxLength(Sku.MaxLength);

        builder.Property(i => i.Name).HasMaxLength(200).IsRequired();
        builder.Property(i => i.Description).HasMaxLength(500);
        builder.Property(i => i.Status).HasConversion<int>();
        builder.Property(i => i.WarehouseId).IsRequired();

        builder.Property(i => i.Quantity)
            .HasConversion(q => q.Value, v => Quantity.Create(v))
            .IsRequired();

        builder.Property(i => i.Location)
            .HasConversion(l => l.Value, v => Location.Create(v))
            .HasMaxLength(200);

        builder.HasIndex(i => i.Sku);
        builder.HasIndex(i => i.WarehouseId);
    }
}
