using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.OrganizationUnits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.OrganizationEngine.Infrastructure.Configurations;

/// <summary>
/// Configures the OrganizationUnit entity schema.
/// </summary>
public class OrganizationUnitConfiguration : IEntityTypeConfiguration<OrganizationUnit>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<OrganizationUnit> builder)
    {
        builder.ToTable("OrganizationUnits");

        builder.HasKey(ou => ou.Id);
        builder.Property(ou => ou.Id)
            .HasConversion(id => id.Value, value => OrganizationUnitId.From(value));

        builder.Property(ou => ou.Name)
            .HasMaxLength(200);

        builder.Property(ou => ou.Code)
            .HasMaxLength(50);

        builder.Property(ou => ou.Description)
            .HasMaxLength(500);

        builder.Property(ou => ou.Status)
            .HasConversion<int>();

        builder.Property(ou => ou.OrganizationId)
            .HasConversion(id => id.Value, value => OrganizationId.From(value))
            .IsRequired();

        builder.Property(ou => ou.OrganizationUnitTypeId)
            .HasConversion(id => id.Value, value => OrganizationUnitTypeId.From(value))
            .IsRequired();

        builder.HasOne<OrganizationUnit>()
            .WithMany()
            .HasForeignKey(ou => ou.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
