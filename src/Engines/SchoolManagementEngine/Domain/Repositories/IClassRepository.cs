using Eleraki.SchoolManagementEngine.Domain.Classes;

namespace Eleraki.SchoolManagementEngine.Domain.Repositories;

public interface IClassRepository
{
    Task<Class?> GetByIdAsync(ClassId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Class>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Class classEntity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Class classEntity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Class classEntity, CancellationToken cancellationToken = default);
}
