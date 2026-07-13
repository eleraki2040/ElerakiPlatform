using Eleraki.SchoolManagementEngine.Domain.Students;

namespace Eleraki.SchoolManagementEngine.Domain.Repositories;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(StudentId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Student student, CancellationToken cancellationToken = default);
    Task UpdateAsync(Student student, CancellationToken cancellationToken = default);
    Task DeleteAsync(Student student, CancellationToken cancellationToken = default);
}
