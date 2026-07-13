using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.HREngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.HREngine.Infrastructure.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly HRDbContext _context;

    public PositionRepository(HRDbContext context)
    {
        _context = context;
    }

    public async Task<Position?> GetByIdAsync(PositionId id, CancellationToken cancellationToken = default)
    {
        return await _context.Positions.FirstOrDefaultAsync(p => p.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Position>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Positions.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Position position, CancellationToken cancellationToken = default)
    {
        await _context.Positions.AddAsync(position, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Position position, CancellationToken cancellationToken = default)
    {
        _context.Positions.Update(position);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Position position, CancellationToken cancellationToken = default)
    {
        _context.Positions.Remove(position);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
