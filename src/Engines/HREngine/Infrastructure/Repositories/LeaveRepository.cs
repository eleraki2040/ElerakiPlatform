using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.HREngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.HREngine.Infrastructure.Repositories;

public class LeaveRepository : ILeaveRepository
{
    private readonly HRDbContext _context;

    public LeaveRepository(HRDbContext context)
    {
        _context = context;
    }

    public async Task<Leave?> GetByIdAsync(LeaveId id, CancellationToken cancellationToken = default)
    {
        return await _context.Leaves.FirstOrDefaultAsync(l => l.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Leave>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.Leaves.Where(l => l.EmployeeId == employeeId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Leave leave, CancellationToken cancellationToken = default)
    {
        await _context.Leaves.AddAsync(leave, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Leave leave, CancellationToken cancellationToken = default)
    {
        _context.Leaves.Update(leave);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Leave leave, CancellationToken cancellationToken = default)
    {
        _context.Leaves.Remove(leave);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
