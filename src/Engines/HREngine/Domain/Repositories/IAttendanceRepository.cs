using Eleraki.HREngine.Domain;

namespace Eleraki.HREngine.Domain.Repositories;

public interface IAttendanceRepository
{
    Task<Attendance?> GetByIdAsync(AttendanceId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Attendance>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task AddAsync(Attendance attendance, CancellationToken cancellationToken = default);
    Task UpdateAsync(Attendance attendance, CancellationToken cancellationToken = default);
    Task DeleteAsync(Attendance attendance, CancellationToken cancellationToken = default);
}
