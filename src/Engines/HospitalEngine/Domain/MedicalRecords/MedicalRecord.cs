using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.HospitalEngine.Domain.MedicalRecords;

public sealed class MedicalRecord : AggregateRoot<MedicalRecordId>
{
    public PatientId PatientId { get; private set; }
    public DoctorId DoctorId { get; private set; }
    public string Diagnosis { get; private set; } = string.Empty;
    public string? Treatment { get; private set; }
    public string? Notes { get; private set; }
    public DateTime RecordDate { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private MedicalRecord(MedicalRecordId id) : base(id)
    {
        PatientId = default!;
        DoctorId = default!;
        Diagnosis = default!;
    }

    public static MedicalRecord Create(PatientId patientId, DoctorId doctorId, string diagnosis, string? treatment = null, string? notes = null)
    {
        Guard.NotNull(patientId, nameof(patientId));
        Guard.NotNull(doctorId, nameof(doctorId));
        Guard.NotNullOrEmpty(diagnosis, nameof(diagnosis));

        var record = new MedicalRecord(MedicalRecordId.New())
        {
            PatientId = patientId,
            DoctorId = doctorId,
            Diagnosis = diagnosis,
            Treatment = treatment,
            Notes = notes,
            RecordDate = Clock.UtcNow,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        record.RaiseDomainEvent(new MedicalRecordCreatedDomainEvent(record.Id, Guid.NewGuid(), Clock.UtcNow));

        return record;
    }

    public void Update(string diagnosis, string? treatment = null, string? notes = null)
    {
        Guard.NotNullOrEmpty(diagnosis, nameof(diagnosis));

        Diagnosis = diagnosis;
        Treatment = treatment;
        Notes = notes;
        ModifiedOn = Clock.UtcNow;
    }
}
