using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.HREngine.Infrastructure.Persistence;

public class HRDbContext : DbContext
{
    public HRDbContext(DbContextOptions<HRDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Salary> Salaries => Set<Salary>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Leave> Leaves => Set<Leave>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new PositionConfiguration());
        modelBuilder.ApplyConfiguration(new SalaryConfiguration());
        modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
        modelBuilder.ApplyConfiguration(new LeaveConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
