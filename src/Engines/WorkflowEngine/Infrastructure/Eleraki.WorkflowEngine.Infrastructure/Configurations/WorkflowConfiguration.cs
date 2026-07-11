using Eleraki.WorkflowEngine.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.WorkflowEngine.Infrastructure.Configurations;

/// <summary>
/// Entity configuration for Workflow aggregate.
/// </summary>
public class WorkflowConfiguration : IEntityTypeConfiguration<Workflow>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Workflow> builder)
    {
        builder.ToTable("Workflows");

        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id)
            .HasConversion(id => id.Value, value => WorkflowId.From(value));

        builder.Property(w => w.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(w => w.Description)
            .HasMaxLength(500);

        builder.Property(w => w.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(w => w.CreatedOn)
            .IsRequired();

        builder.Property(w => w.ModifiedOn)
            .IsRequired();
    }
}
