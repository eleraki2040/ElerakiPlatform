using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.FinanceEngine.Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasConversion(id => id.Value, value => TransactionId.From(value));

        builder.Property(t => t.AccountId).HasConversion(id => id.Value, value => AccountId.From(value));

        builder.Property(t => t.Amount)
            .HasConversion(m => m.Amount, v => Money.Create(v, "USD"));

        builder.Property(t => t.Description).HasMaxLength(500);
        builder.Property(t => t.Type).HasConversion<int>();
        builder.Property(t => t.Status).HasConversion<int>();
        builder.Property(t => t.TransactionDate).IsRequired();
        builder.Property(t => t.CreatedOn).IsRequired();
        builder.Property(t => t.ModifiedOn).IsRequired();

        builder.HasIndex(t => t.AccountId);
    }
}
