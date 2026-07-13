using Eleraki.HREngine.Domain;

namespace Eleraki.HREngine.Domain.Repositories;

public interface ISalaryRepository
{
    Task<Salary?> GetByIdAsync(SalaryId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Salary>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
    Task AddAsync(Salary salary, CancellationToken cancellationToken = default);
    Task UpdateAsync(Salary salary, CancellationToken cancellationToken = default);
    Task DeleteAsync(Salary salary, CancellationToken cancellationToken = default);
}
