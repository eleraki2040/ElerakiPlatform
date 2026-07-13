using Eleraki.SchoolManagementEngine.Domain.Teachers;

namespace Eleraki.SchoolManagementEngine.Domain.Repositories;

public interface ITeacherRepository
{
    Task<Teacher?> GetByIdAsync(TeacherId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Teacher>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default);
    Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default);
    Task DeleteAsync(Teacher teacher, CancellationToken cancellationToken = default);
}
