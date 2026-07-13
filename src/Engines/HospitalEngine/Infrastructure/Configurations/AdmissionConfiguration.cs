using Eleraki.HospitalEngine.Domain.Admissions;
using Eleraki.HospitalEngine.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.HospitalEngine.Infrastructure.Configurations;

public class AdmissionConfiguration : IEntityTypeConfiguration<Admission>
{
    public void Configure(EntityTypeBuilder<Admission> builder)
    {
        builder.ToTable("Admissions");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(id => id.Value, value => AdmissionId.From(value));

        builder.Property(a => a.PatientId)
            .HasConversion(id => id.Value, value => PatientId.From(value));

        builder.Property(a => a.BedId)
            .HasConversion(id => id!.Value, value => BedId.From(value));
    }
}
