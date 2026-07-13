using Eleraki.HospitalEngine.Domain.Admissions;
using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Billing;
using Eleraki.HospitalEngine.Domain.Patients;

namespace Eleraki.HospitalEngine.Domain.Repositories;

public interface IHospitalRepository
{
    Task<Patient?> GetPatientByIdAsync(PatientId id, CancellationToken cancellationToken = default);
    Task AddPatientAsync(Patient patient, CancellationToken cancellationToken = default);

    Task<Appointment?> GetAppointmentByIdAsync(AppointmentId id, CancellationToken cancellationToken = default);
    Task AddAppointmentAsync(Appointment appointment, CancellationToken cancellationToken = default);

    Task<Admission?> GetAdmissionByIdAsync(AdmissionId id, CancellationToken cancellationToken = default);
    Task AddAdmissionAsync(Admission admission, CancellationToken cancellationToken = default);

    Task<Invoice?> GetInvoiceByIdAsync(InvoiceId id, CancellationToken cancellationToken = default);
    Task AddInvoiceAsync(Invoice invoice, CancellationToken cancellationToken = default);
}
