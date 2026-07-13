using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.HREngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.HREngine.Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly HRDbContext _context;

    public AttendanceRepository(HRDbContext context)
    {
        _context = context;
    }

    public async Task<Attendance?> GetByIdAsync(AttendanceId id, CancellationToken cancellationToken = default)
    {
        return await _context.Attendances.FirstOrDefaultAsync(a => a.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Attendance>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.Attendances.Where(a => a.EmployeeId == employeeId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Attendance attendance, CancellationToken cancellationToken = default)
    {
        await _context.Attendances.AddAsync(attendance, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Attendance attendance, CancellationToken cancellationToken = default)
    {
        _context.Attendances.Update(attendance);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Attendance attendance, CancellationToken cancellationToken = default)
    {
        _context.Attendances.Remove(attendance);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
