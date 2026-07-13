using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.SchoolManagementEngine.Infrastructure.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasConversion(id => id.Value, value => TeacherId.From(value));

        builder.Property(t => t.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(t => t.LastName).HasMaxLength(100).IsRequired();
        builder.Property(t => t.Email).HasMaxLength(200).IsRequired();
        builder.Property(t => t.PhoneNumber).HasMaxLength(20);
        builder.Property(t => t.Specialization).HasMaxLength(200).IsRequired();
        builder.Property(t => t.HireDate).IsRequired();
        builder.Property(t => t.IsActive).IsRequired();

        builder.HasIndex(t => t.Email).IsUnique();
    }
}
