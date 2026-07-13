using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Admissions;
using Eleraki.HospitalEngine.Domain.Billing;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.HospitalEngine.Domain;

public sealed class Patient : AggregateRoot<PatientId>
{
    public PersonName Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public PhoneNumber Phone { get; private set; } = null!;
    public DateTime DateOfBirth { get; private set; }
    public string? EmergencyContact { get; private set; }
    public string? InsuranceNumber { get; private set; }
    public PatientStatus Status { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Patient(PatientId id) : base(id)
    {
        Name = default!;
        Email = default!;
        Phone = default!;
    }

    private Patient() : base(default!)
    {
    }

    public static Patient Create(PersonName name, Email email, PhoneNumber phone, DateTime dateOfBirth, string? emergencyContact = null, string? insuranceNumber = null)
    {
        Guard.NotNull(name, nameof(name));
        Guard.NotNull(email, nameof(email));
        Guard.NotNull(phone, nameof(phone));
        Guard.NotNull(dateOfBirth, nameof(dateOfBirth));

        var patient = new Patient(PatientId.New())
        {
            Name = name,
            Email = email,
            Phone = phone,
            DateOfBirth = dateOfBirth,
            EmergencyContact = emergencyContact,
            InsuranceNumber = insuranceNumber,
            Status = PatientStatus.Active,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        patient.RaiseDomainEvent(new PatientRegisteredDomainEvent(patient.Id, Guid.NewGuid(), Clock.UtcNow));

        return patient;
    }

    public void Update(PersonName name, Email email, PhoneNumber phone, string? emergencyContact = null)
    {
        Guard.NotNull(name, nameof(name));
        Guard.NotNull(email, nameof(email));
        Guard.NotNull(phone, nameof(phone));

        Name = name;
        Email = email;
        Phone = phone;
        EmergencyContact = emergencyContact;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PatientUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Deactivate()
    {
        if (Status == PatientStatus.Inactive)
            return;

        Status = PatientStatus.Inactive;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PatientDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Activate()
    {
        if (Status == PatientStatus.Active)
            return;

        Status = PatientStatus.Active;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PatientActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum PatientStatus
{
    Active = 1,
    Inactive = 2,
    Archived = 3
}
