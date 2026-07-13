using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SalesEngine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.SalesEngine.Infrastructure.Configurations;

public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.ToTable("SalesOrders");

        builder.HasKey(so => so.Id);
        builder.Property(so => so.Id)
            .HasConversion(id => id.Value, value => SalesOrderId.From(value));

        builder.Property(so => so.OrderNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(so => so.CustomerId)
            .HasConversion(id => id.Value, value => CustomerId.From(value))
            .IsRequired();

        builder.Property(so => so.CustomerName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(so => so.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.OwnsOne(so => so.TotalAmount, money =>
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

        builder.HasMany<SalesOrderLine>()
            .WithOne()
            .HasForeignKey(sl => sl.SalesOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
