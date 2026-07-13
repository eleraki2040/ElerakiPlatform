using Eleraki.HREngine.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.HREngine.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(id => id.Value, value => EmployeeId.From(value));

        builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(255).IsRequired();
        builder.Property(e => e.Phone).HasMaxLength(50).IsRequired();
        builder.Property(e => e.DateOfBirth).IsRequired();
        builder.Property(e => e.DepartmentId).IsRequired();
        builder.Property(e => e.PositionId).IsRequired();
        builder.Property(e => e.SalaryId).HasMaxLength(100);
        builder.Property(e => e.Status).HasConversion<int>();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();

        builder.HasIndex(e => e.Email).IsUnique();
    }
}
