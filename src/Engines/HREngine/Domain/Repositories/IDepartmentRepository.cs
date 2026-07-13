using Eleraki.HREngine.Domain;

namespace Eleraki.HREngine.Domain.Repositories;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(DepartmentId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Department department, CancellationToken cancellationToken = default);
    Task UpdateAsync(Department department, CancellationToken cancellationToken = default);
    Task DeleteAsync(Department department, CancellationToken cancellationToken = default);
}
