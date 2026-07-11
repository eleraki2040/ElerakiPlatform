using Eleraki.Enterprise.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.Enterprise.Infrastructure.Configurations;

/// <summary>
/// Entity configuration for Party aggregate.
/// </summary>
public class PartyConfiguration : IEntityTypeConfiguration<Party>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Party> builder)
    {
        builder.ToTable("Parties");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => PartyId.From(value));

        builder.Property(p => p.Name)
            .HasConversion(name => name.Value, value => PartyName.Create(value))
            .HasMaxLength(PartyName.MaxLength)
            .IsRequired();

        builder.Property(p => p.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.CreatedOn)
            .IsRequired();

        builder.Property(p => p.ModifiedOn)
            .IsRequired();
    }
}
