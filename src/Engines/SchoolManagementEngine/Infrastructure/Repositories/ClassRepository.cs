using Eleraki.SchoolManagementEngine.Domain.Repositories;
using Eleraki.SchoolManagementEngine.Domain.Classes;
using Eleraki.SchoolManagementEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.SchoolManagementEngine.Infrastructure.Repositories;

public class ClassRepository : IClassRepository
{
    private readonly SchoolDbContext _context;

    public ClassRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<Class?> GetByIdAsync(ClassId id, CancellationToken cancellationToken = default)
    {
        return await _context.Classes.FirstOrDefaultAsync(c => c.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Class>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Classes.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Class classEntity, CancellationToken cancellationToken = default)
    {
        await _context.Classes.AddAsync(classEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Class classEntity, CancellationToken cancellationToken = default)
    {
        _context.Classes.Update(classEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Class classEntity, CancellationToken cancellationToken = default)
    {
        _context.Classes.Remove(classEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
