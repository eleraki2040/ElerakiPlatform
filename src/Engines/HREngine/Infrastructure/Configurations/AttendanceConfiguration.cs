using Eleraki.HREngine.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.HREngine.Infrastructure.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable("Attendances");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(id => id.Value, value => AttendanceId.From(value));

        builder.Property(a => a.EmployeeId).IsRequired();
        builder.Property(a => a.AttendanceDate).IsRequired();
        builder.Property(a => a.CheckInTime);
        builder.Property(a => a.CheckOutTime);
        builder.Property(a => a.Status).HasConversion<int>();
        builder.Property(a => a.Notes).HasMaxLength(500);
        builder.Property(a => a.CreatedAt).IsRequired();
        builder.Property(a => a.UpdatedAt).IsRequired();

        builder.HasIndex(a => new { a.EmployeeId, a.AttendanceDate }).IsUnique();
    }
}
