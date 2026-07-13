using Eleraki.SchoolManagementEngine.Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.SchoolManagementEngine.Infrastructure.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasConversion(id => id.Value, value => StudentId.From(value));

        builder.Property(s => s.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(s => s.LastName).HasMaxLength(100).IsRequired();
        builder.Property(s => s.Email).HasMaxLength(200).IsRequired();
        builder.Property(s => s.DateOfBirth).IsRequired();
        builder.Property(s => s.Address).HasMaxLength(500);
        builder.Property(s => s.PhoneNumber).HasMaxLength(20);
        builder.Property(s => s.EnrollmentDate).IsRequired();
        builder.Property(s => s.IsActive).IsRequired();

        builder.HasIndex(s => s.Email).IsUnique();
    }
}
