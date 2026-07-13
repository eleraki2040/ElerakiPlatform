using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Courses;
using Eleraki.SchoolManagementEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.SchoolManagementEngine.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly SchoolDbContext _context;

    public CourseRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<Course?> GetByIdAsync(CourseId id, CancellationToken cancellationToken = default)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Course>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Courses.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Course course, CancellationToken cancellationToken = default)
    {
        await _context.Courses.AddAsync(course, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Course course, CancellationToken cancellationToken = default)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Course course, CancellationToken cancellationToken = default)
    {
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
