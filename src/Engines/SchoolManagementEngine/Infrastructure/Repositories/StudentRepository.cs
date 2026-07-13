using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Students;
using Eleraki.SchoolManagementEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.SchoolManagementEngine.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly SchoolDbContext _context;

    public StudentRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<Student?> GetByIdAsync(StudentId id, CancellationToken cancellationToken = default)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Students.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _context.Students.AddAsync(student, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Student student, CancellationToken cancellationToken = default)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
