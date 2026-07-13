using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.OrganizationUnitTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.OrganizationEngine.Infrastructure.Configurations;

/// <summary>
/// Configures the OrganizationUnitType entity schema.
/// </summary>
public class OrganizationUnitTypeConfiguration : IEntityTypeConfiguration<OrganizationUnitType>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<OrganizationUnitType> builder)
    {
        builder.ToTable("OrganizationUnitTypes");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(id => id.Value, value => OrganizationUnitTypeId.From(value));

        builder.Property(t => t.Name)
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.IsActive)
            .IsRequired();
    }
}
