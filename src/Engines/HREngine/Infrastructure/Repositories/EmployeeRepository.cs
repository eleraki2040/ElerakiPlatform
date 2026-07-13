using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.HREngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.HREngine.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly HRDbContext _context;

    public EmployeeRepository(HRDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(EmployeeId id, CancellationToken cancellationToken = default)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Employees.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await _context.Employees.AddAsync(employee, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Employees.AnyAsync(e => e.Email == email, cancellationToken);
    }
}
