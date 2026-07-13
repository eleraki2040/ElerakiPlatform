using Eleraki.HREngine.Domain;

namespace Eleraki.HREngine.Domain.Repositories;

public interface ILeaveRepository
{
    Task<Leave?> GetByIdAsync(LeaveId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Leave>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task AddAsync(Leave leave, CancellationToken cancellationToken = default);
    Task UpdateAsync(Leave leave, CancellationToken cancellationToken = default);
    Task DeleteAsync(Leave leave, CancellationToken cancellationToken = default);
}
