using Eleraki.FinanceEngine.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.FinanceEngine.Infrastructure.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(id => id.Value, value => AccountId.From(value));

        builder.Property(a => a.Name).HasMaxLength(200).IsRequired();
        builder.Property(a => a.Code).HasMaxLength(20).IsRequired();
        builder.Property(a => a.Type).HasConversion<int>();
        builder.Property(a => a.ParentAccountId).HasMaxLength(100);
        builder.Property(a => a.IsActive).HasDefaultValue(true);
        builder.Property(a => a.CreatedOn).IsRequired();
        builder.Property(a => a.ModifiedOn).IsRequired();

        builder.HasIndex(a => a.Code).IsUnique();
    }
}
