using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Admissions;
using Eleraki.HospitalEngine.Domain.Billing;
using Eleraki.HospitalEngine.Domain.MedicalRecords;
using Eleraki.HospitalEngine.Domain.Prescriptions;
using Eleraki.SharedKernel.Events;

namespace Eleraki.HospitalEngine.Domain.Events;

public sealed record PatientRegisteredDomainEvent(PatientId PatientId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PatientUpdatedDomainEvent(PatientId PatientId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PatientActivatedDomainEvent(PatientId PatientId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PatientDeactivatedDomainEvent(PatientId PatientId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record AppointmentScheduledDomainEvent(AppointmentId AppointmentId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record AppointmentCompletedDomainEvent(AppointmentId AppointmentId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record AppointmentCancelledDomainEvent(AppointmentId AppointmentId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record AdmissionCreatedDomainEvent(AdmissionId AdmissionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DischargeProcessedDomainEvent(AdmissionId AdmissionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record InvoiceGeneratedDomainEvent(InvoiceId InvoiceId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PaymentReceivedDomainEvent(InvoiceId InvoiceId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record MedicalRecordCreatedDomainEvent(MedicalRecordId MedicalRecordId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PrescriptionIssuedDomainEvent(PrescriptionId PrescriptionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
