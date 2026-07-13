using Eleraki.FinanceEngine.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.FinanceEngine.Infrastructure.Configurations;

public class JournalEntryLineConfiguration : IEntityTypeConfiguration<JournalEntryLine>
{
    public void Configure(EntityTypeBuilder<JournalEntryLine> builder)
    {
        builder.ToTable("JournalEntryLines");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Description).HasMaxLength(500).IsRequired();
        builder.Property(l => l.Type).HasConversion<int>();
        builder.Property(l => l.LineNumber).IsRequired();
        builder.Property(l => l.JournalEntryId).HasConversion(id => id.Value, value => Eleraki.FinanceEngine.Domain.JournalEntryId.From(value));
        builder.Property(l => l.AccountId).HasConversion(id => id.Value, value => Eleraki.FinanceEngine.Domain.AccountId.From(value));

        builder.Property(l => l.Amount)
            .HasConversion(m => m.Amount, v => Eleraki.SharedKernel.ValueObjects.Money.Create(v, "USD"));

        builder.HasIndex(l => new { l.JournalEntryId, l.LineNumber }).IsUnique();
    }
}
