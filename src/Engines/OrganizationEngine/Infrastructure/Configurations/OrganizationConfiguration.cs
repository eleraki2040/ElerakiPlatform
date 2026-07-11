using Eleraki.OrganizationEngine.Domain.Organizations;
using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.OrganizationEngine.Infrastructure.Configurations;

/// <summary>
/// Configures the Organization entity schema.
/// </summary>
public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasConversion(id => id.Value, value => OrganizationId.From(value));

        builder.Property(o => o.Name)
            .HasConversion(name => name.Value, value => OrganizationName.Create(value))
            .HasMaxLength(OrganizationName.MaxLength);

        builder.Property(o => o.Description)
            .HasMaxLength(500);

        builder.Property(o => o.Status)
            .HasConversion<int>();

        builder.Property(o => o.Code)
            .HasConversion(code => code.Value, value => OrganizationCode.Create(value))
            .HasMaxLength(OrganizationCode.MaxLength);
    }
}
