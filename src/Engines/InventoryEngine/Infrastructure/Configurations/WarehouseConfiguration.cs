using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.InventoryEngine.Infrastructure.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");

        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id).HasConversion(id => id.Value, value => WarehouseId.From(value));

        builder.Property(w => w.Name).HasMaxLength(200).IsRequired();
        builder.Property(w => w.Code).HasMaxLength(20).IsRequired();
        builder.Property(w => w.Address).HasMaxLength(500);
        builder.Property(w => w.Status).HasConversion<int>();

        builder.HasIndex(w => w.Code).IsUnique();
    }
}
