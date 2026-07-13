using Eleraki.SchoolManagementEngine.Domain.Courses;

namespace Eleraki.SchoolManagementEngine.Domain.Repositories;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(CourseId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Course>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Course course, CancellationToken cancellationToken = default);
    Task UpdateAsync(Course course, CancellationToken cancellationToken = default);
    Task DeleteAsync(Course course, CancellationToken cancellationToken = default);
}
