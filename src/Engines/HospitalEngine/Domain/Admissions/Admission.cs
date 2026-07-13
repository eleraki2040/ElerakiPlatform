using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.HospitalEngine.Domain.Admissions;

public sealed class Admission : AggregateRoot<AdmissionId>
{
    public PatientId PatientId { get; private set; }
    public BedId? BedId { get; private set; }
    public AdmissionStatus Status { get; private set; }
    public DateTime AdmittedAt { get; private set; }
    public DateTime? DischargedAt { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Admission(AdmissionId id) : base(id)
    {
        PatientId = default!;
    }

    private Admission() : base(default!)
    {
    }

    public static Admission Create(PatientId patientId, BedId? bedId = null, string? notes = null)
    {
        Guard.NotNull(patientId, nameof(patientId));

        var admission = new Admission(AdmissionId.New())
        {
            PatientId = patientId,
            BedId = bedId,
            Status = AdmissionStatus.Admitted,
            AdmittedAt = Clock.UtcNow,
            Notes = notes,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        admission.RaiseDomainEvent(new AdmissionCreatedDomainEvent(admission.Id, Guid.NewGuid(), Clock.UtcNow));

        return admission;
    }

    public void AssignBed(BedId bedId)
    {
        BedId = bedId;
        ModifiedOn = Clock.UtcNow;
    }

    public void Discharge()
    {
        if (Status == AdmissionStatus.Discharged)
            return;

        Status = AdmissionStatus.Discharged;
        DischargedAt = Clock.UtcNow;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new DischargeProcessedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum AdmissionStatus
{
    Admitted = 1,
    Discharged = 2,
    Transferred = 3
}
