using Eleraki.HospitalEngine.Domain;
using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Admissions;
using Eleraki.HospitalEngine.Domain.Billing;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.HospitalEngine.Infrastructure.Persistence;

public class HospitalDbContext : DbContext
{
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Admission> Admissions => Set<Admission>();
    public DbSet<Invoice> Invoices => Set<Invoice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
