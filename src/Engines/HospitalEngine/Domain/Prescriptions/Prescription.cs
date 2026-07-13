using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.MedicalRecords;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.HospitalEngine.Domain.Prescriptions;

public sealed class Prescription : AggregateRoot<PrescriptionId>
{
    public MedicalRecordId MedicalRecordId { get; private set; }
    public PatientId PatientId { get; private set; }
    public DoctorId DoctorId { get; private set; }
    public string MedicationName { get; private set; } = string.Empty;
    public string Dosage { get; private set; } = string.Empty;
    public string Frequency { get; private set; } = string.Empty;
    public int DurationDays { get; private set; }
    public PrescriptionStatus Status { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Prescription(PrescriptionId id) : base(id)
    {
        MedicalRecordId = default!;
        PatientId = default!;
        DoctorId = default!;
        MedicationName = default!;
        Dosage = default!;
        Frequency = default!;
    }

    public static Prescription Create(MedicalRecordId medicalRecordId, PatientId patientId, DoctorId doctorId, string medicationName, string dosage, string frequency, int durationDays)
    {
        Guard.NotNull(medicalRecordId, nameof(medicalRecordId));
        Guard.NotNull(patientId, nameof(patientId));
        Guard.NotNull(doctorId, nameof(doctorId));
        Guard.NotNullOrEmpty(medicationName, nameof(medicationName));
        Guard.NotNullOrEmpty(dosage, nameof(dosage));
        Guard.NotNullOrEmpty(frequency, nameof(frequency));
        Guard.Ensure(durationDays > 0, "Duration must be greater than zero.");

        var prescription = new Prescription(PrescriptionId.New())
        {
            MedicalRecordId = medicalRecordId,
            PatientId = patientId,
            DoctorId = doctorId,
            MedicationName = medicationName,
            Dosage = dosage,
            Frequency = frequency,
            DurationDays = durationDays,
            Status = PrescriptionStatus.Pending,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        prescription.RaiseDomainEvent(new PrescriptionIssuedDomainEvent(prescription.Id, Guid.NewGuid(), Clock.UtcNow));

        return prescription;
    }

    public void Dispense()
    {
        if (Status == PrescriptionStatus.Dispensed)
            return;

        Status = PrescriptionStatus.Dispensed;
        ModifiedOn = Clock.UtcNow;
    }

    public void Cancel()
    {
        if (Status == PrescriptionStatus.Cancelled)
            return;

        Status = PrescriptionStatus.Cancelled;
        ModifiedOn = Clock.UtcNow;
    }
}

public enum PrescriptionStatus
{
    Pending = 1,
    Dispensed = 2,
    Cancelled = 3
}
