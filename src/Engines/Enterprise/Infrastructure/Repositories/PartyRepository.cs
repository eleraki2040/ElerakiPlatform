using Eleraki.Enterprise.Domain;
using Eleraki.Enterprise.Domain.Repositories;
using Eleraki.Enterprise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.Enterprise.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Party aggregate.
/// </summary>
public sealed class PartyRepository : IPartyRepository
{
    private readonly EnterpriseDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="PartyRepository"/> class.
    /// </summary>
    /// <param name="context">The Enterprise DbContext.</param>
    public PartyRepository(EnterpriseDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Party?> GetByIdAsync(PartyId id, CancellationToken cancellationToken = default)
    {
        return await _context.Parties
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Party>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Parties
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(Party entity, CancellationToken cancellationToken = default)
    {
        await _context.Parties.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Party entity, CancellationToken cancellationToken = default)
    {
        _context.Parties.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Party entity, CancellationToken cancellationToken = default)
    {
        _context.Parties.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
