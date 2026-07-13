using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.PurchasingEngine.Infrastructure.Configurations;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.ToTable("Vendors");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasConversion(id => id.Value, value => VendorId.From(value));

        builder.Property(v => v.Name).HasMaxLength(200).IsRequired();
        builder.Property(v => v.ContactEmail).HasMaxLength(200);
        builder.Property(v => v.ContactPhone).HasMaxLength(50);
        builder.Property(v => v.Address).HasMaxLength(500);
        builder.Property(v => v.Status).HasConversion<int>();
    }
}
