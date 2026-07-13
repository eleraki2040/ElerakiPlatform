using Eleraki.SchoolManagementEngine.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.SchoolManagementEngine.Infrastructure.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(id => id.Value, value => CourseId.From(value));

        builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
        builder.Property(c => c.Code).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(1000);
        builder.Property(c => c.Credits).IsRequired();
        builder.Property(c => c.IsActive).IsRequired();

        builder.HasIndex(c => c.Code).IsUnique();
    }
}
