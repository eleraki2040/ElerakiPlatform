using Eleraki.HospitalEngine.Domain;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.HospitalEngine.Infrastructure.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(id => id.Value, value => PatientId.From(value));

        builder.Property(p => p.Name)
            .HasConversion(name => name.Value, value => PersonName.Create(value))
            .HasMaxLength(PersonName.MaxLength);

        builder.Property(p => p.Email)
            .HasConversion(email => email.Value, value => Email.Create(value))
            .HasMaxLength(Email.MaxLength);

        builder.Property(p => p.Phone)
            .HasConversion(phone => phone.Value, value => PhoneNumber.Create(value))
            .HasMaxLength(PhoneNumber.MaxLength);

        builder.Property(p => p.EmergencyContact).HasMaxLength(200);
        builder.Property(p => p.InsuranceNumber).HasMaxLength(100);
        builder.Property(p => p.Status).HasConversion<int>();
    }
}
