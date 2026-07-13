using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.HREngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.HREngine.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly HRDbContext _context;

    public DepartmentRepository(HRDbContext context)
    {
        _context = context;
    }

    public async Task<Department?> GetByIdAsync(DepartmentId id, CancellationToken cancellationToken = default)
    {
        return await _context.Departments.FirstOrDefaultAsync(d => d.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departments.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Department department, CancellationToken cancellationToken = default)
    {
        await _context.Departments.AddAsync(department, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Department department, CancellationToken cancellationToken = default)
    {
        _context.Departments.Update(department);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Department department, CancellationToken cancellationToken = default)
    {
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
