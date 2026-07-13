using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.HospitalEngine.Domain.Appointments;

public sealed class Appointment : AggregateRoot<AppointmentId>
{
    public PatientId PatientId { get; private set; }
    public DoctorId DoctorId { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Appointment(AppointmentId id) : base(id)
    {
        PatientId = default!;
        DoctorId = default!;
    }

    private Appointment() : base(default!)
    {
    }

    public static Appointment Create(PatientId patientId, DoctorId doctorId, DateTime scheduledAt, string? notes = null)
    {
        Guard.NotNull(patientId, nameof(patientId));
        Guard.NotNull(doctorId, nameof(doctorId));
        Guard.NotNull(scheduledAt, nameof(scheduledAt));

        var appointment = new Appointment(AppointmentId.New())
        {
            PatientId = patientId,
            DoctorId = doctorId,
            ScheduledAt = scheduledAt,
            Status = AppointmentStatus.Scheduled,
            Notes = notes,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        appointment.RaiseDomainEvent(new AppointmentScheduledDomainEvent(appointment.Id, Guid.NewGuid(), Clock.UtcNow));

        return appointment;
    }

    public void Complete()
    {
        if (Status == AppointmentStatus.Completed)
            return;

        Status = AppointmentStatus.Completed;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new AppointmentCompletedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Cancel()
    {
        if (Status == AppointmentStatus.Cancelled)
            return;

        Status = AppointmentStatus.Cancelled;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new AppointmentCancelledDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum AppointmentStatus
{
    Scheduled = 1,
    Completed = 2,
    Cancelled = 3,
    NoShow = 4
}
