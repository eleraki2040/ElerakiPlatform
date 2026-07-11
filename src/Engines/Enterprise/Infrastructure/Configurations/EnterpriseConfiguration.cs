using Eleraki.Enterprise.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.Enterprise.Infrastructure.Configurations;

/// <summary>
/// Configures the Enterprise entity schema.
/// </summary>
public class EnterpriseConfiguration : IEntityTypeConfiguration<Eleraki.Enterprise.Domain.Enterprise>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Eleraki.Enterprise.Domain.Enterprise> builder)
    {
        builder.ToTable("Enterprises");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(id => id.Value, value => EnterpriseId.From(value));

        builder.Property(e => e.Code)
            .HasConversion(code => code.Value, value => EnterpriseCode.Create(value))
            .HasMaxLength(EnterpriseCode.MaxLength);

        builder.Property(e => e.Name)
            .HasConversion(name => name.Value, value => EnterpriseName.Create(value))
            .HasMaxLength(EnterpriseName.MaxLength);

        builder.Property(e => e.LegalName)
            .HasMaxLength(500);

        builder.Property(e => e.Status)
            .HasConversion<int>();

        builder.Property(e => e.CreatedOn);
        builder.Property(e => e.ModifiedOn);
    }
}
