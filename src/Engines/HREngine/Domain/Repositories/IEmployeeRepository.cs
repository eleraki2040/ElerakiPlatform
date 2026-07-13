using Eleraki.HREngine.Domain;

namespace Eleraki.HREngine.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(EmployeeId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
    Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
}
