using Eleraki.HospitalEngine.Domain;
using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Admissions;
using Eleraki.HospitalEngine.Domain.Billing;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Repositories;
using Eleraki.HospitalEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.HospitalEngine.Infrastructure.Repositories;

public class HospitalRepository : IHospitalRepository
{
    private readonly HospitalDbContext _context;

    public HospitalRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<Patient?> GetPatientByIdAsync(PatientId id, CancellationToken cancellationToken = default)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.Id.Value == id.Value, cancellationToken);
    }

    public async Task AddPatientAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        await _context.Patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(AppointmentId id, CancellationToken cancellationToken = default)
    {
        return await _context.Appointments.FirstOrDefaultAsync(a => a.Id.Value == id.Value, cancellationToken);
    }

    public async Task AddAppointmentAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        await _context.Appointments.AddAsync(appointment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Admission?> GetAdmissionByIdAsync(AdmissionId id, CancellationToken cancellationToken = default)
    {
        return await _context.Admissions.FirstOrDefaultAsync(a => a.Id.Value == id.Value, cancellationToken);
    }

    public async Task AddAdmissionAsync(Admission admission, CancellationToken cancellationToken = default)
    {
        await _context.Admissions.AddAsync(admission, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(InvoiceId id, CancellationToken cancellationToken = default)
    {
        return await _context.Invoices.FirstOrDefaultAsync(i => i.Id.Value == id.Value, cancellationToken);
    }

    public async Task AddInvoiceAsync(Invoice invoice, CancellationToken cancellationToken = default)
    {
        await _context.Invoices.AddAsync(invoice, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
