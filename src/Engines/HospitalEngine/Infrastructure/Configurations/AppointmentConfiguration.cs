using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.HospitalEngine.Infrastructure.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(id => id.Value, value => AppointmentId.From(value));

        builder.Property(a => a.PatientId)
            .HasConversion(id => id.Value, value => PatientId.From(value));

        builder.Property(a => a.DoctorId)
            .HasConversion(id => id.Value, value => DoctorId.From(value));

        builder.Property(a => a.Status).HasConversion<int>();
    }
}
