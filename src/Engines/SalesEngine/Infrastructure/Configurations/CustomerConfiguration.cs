using Eleraki.SalesEngine.Domain.Customers;
using Eleraki.SalesEngine.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.SalesEngine.Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(id => id.Value, value => CustomerId.From(value));

        builder.Property(c => c.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Phone)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Address)
            .HasMaxLength(500);

        builder.Property(c => c.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(c => c.Email).IsUnique();
    }
}
