using Eleraki.HREngine.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.HREngine.Infrastructure.Configurations;

public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
{
    public void Configure(EntityTypeBuilder<Leave> builder)
    {
        builder.ToTable("Leaves");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasConversion(id => id.Value, value => LeaveId.From(value));

        builder.Property(l => l.EmployeeId).IsRequired();
        builder.Property(l => l.Type).HasConversion<int>().IsRequired();
        builder.Property(l => l.StartDate).IsRequired();
        builder.Property(l => l.EndDate).IsRequired();
        builder.Property(l => l.Reason).HasMaxLength(1000);
        builder.Property(l => l.Status).HasConversion<int>().IsRequired();
        builder.Property(l => l.ApprovedBy).HasMaxLength(200);
        builder.Property(l => l.ApprovedAt);
        builder.Property(l => l.CreatedAt).IsRequired();
        builder.Property(l => l.UpdatedAt).IsRequired();

        builder.HasIndex(l => l.EmployeeId);
    }
}
