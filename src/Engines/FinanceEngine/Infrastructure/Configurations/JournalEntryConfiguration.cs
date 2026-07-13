using Eleraki.FinanceEngine.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.FinanceEngine.Infrastructure.Configurations;

public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        builder.ToTable("JournalEntries");

        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id).HasConversion(id => id.Value, value => JournalEntryId.From(value));

        builder.Property(j => j.ReferenceNumber).HasMaxLength(100).IsRequired();
        builder.Property(j => j.Description).HasMaxLength(500).IsRequired();
        builder.Property(j => j.Status).HasConversion<int>();
        builder.Property(j => j.EntryDate).IsRequired();
        builder.Property(j => j.CreatedOn).IsRequired();
        builder.Property(j => j.ModifiedOn).IsRequired();

        builder.HasIndex(j => j.ReferenceNumber).IsUnique();
    }
}
