using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SchoolManagementEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.SchoolManagementEngine.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly SchoolDbContext _context;

    public TeacherRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<Teacher?> GetByIdAsync(TeacherId id, CancellationToken cancellationToken = default)
    {
        return await _context.Teachers.FirstOrDefaultAsync(t => t.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Teachers.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        await _context.Teachers.AddAsync(teacher, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
