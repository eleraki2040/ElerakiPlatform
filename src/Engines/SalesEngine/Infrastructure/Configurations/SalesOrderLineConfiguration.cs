using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SalesEngine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.SalesEngine.Infrastructure.Configurations;

public class SalesOrderLineConfiguration : IEntityTypeConfiguration<SalesOrderLine>
{
    public void Configure(EntityTypeBuilder<SalesOrderLine> builder)
    {
        builder.ToTable("SalesOrderLines");

        builder.HasKey(sl => sl.Id);
        builder.Property(sl => sl.Id)
            .HasConversion(id => id.Value, value => SalesOrderLineId.From(value));

        builder.Property(sl => sl.ProductName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(sl => sl.Quantity)
            .HasConversion(q => q.Value, v => Quantity.Create(v))
            .IsRequired();

        builder.OwnsOne(sl => sl.UnitPrice, money =>
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

        builder.OwnsOne(sl => sl.TotalPrice, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("TotalPrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("TotalPriceCurrency")
                .HasMaxLength(3)
                .IsRequired();
        });
    }
}
