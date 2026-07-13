using Eleraki.HospitalEngine.Domain.Billing;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.HospitalEngine.Infrastructure.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasConversion(id => id.Value, value => InvoiceId.From(value));

        builder.Property(i => i.PatientId)
            .HasConversion(id => id.Value, value => PatientId.From(value));

        builder.Property(i => i.TotalAmount)
            .HasConversion(m => m.Amount, v => Money.Create(v, "EGP"));

        builder.Property(i => i.PaidAmount)
            .HasConversion(m => m.Amount, v => Money.Create(v, "EGP"));

        builder.Property(i => i.Status).HasConversion<int>();
    }
}
