using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.HREngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.HREngine.Infrastructure.Repositories;

public class SalaryRepository : ISalaryRepository
{
    private readonly HRDbContext _context;

    public SalaryRepository(HRDbContext context)
    {
        _context = context;
    }

    public async Task<Salary?> GetByIdAsync(SalaryId id, CancellationToken cancellationToken = default)
    {
        return await _context.Salaries.FirstOrDefaultAsync(s => s.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Salary>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.Salaries.Where(s => s.EmployeeId == employeeId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Salary salary, CancellationToken cancellationToken = default)
    {
        await _context.Salaries.AddAsync(salary, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Salary salary, CancellationToken cancellationToken = default)
    {
        _context.Salaries.Update(salary);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Salary salary, CancellationToken cancellationToken = default)
    {
        _context.Salaries.Remove(salary);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
