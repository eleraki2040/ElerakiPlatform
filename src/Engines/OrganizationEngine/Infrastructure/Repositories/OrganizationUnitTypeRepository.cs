using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.OrganizationUnitTypes;
using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.OrganizationEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.OrganizationEngine.Infrastructure.Repositories;

/// <summary>
/// Entity Framework Core implementation of IOrganizationUnitTypeRepository.
/// </summary>
public class OrganizationUnitTypeRepository : IOrganizationUnitTypeRepository
{
    private readonly OrganizationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUnitTypeRepository"/> class.
    /// </summary>
    /// <param name="context">The Organization DbContext.</param>
    public OrganizationUnitTypeRepository(OrganizationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<OrganizationUnitType?> GetByIdAsync(OrganizationUnitTypeId id, CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnitTypes.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrganizationUnitType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnitTypes.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrganizationUnitType>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnitTypes
            .Where(t => t.IsActive)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(OrganizationUnitType organizationUnitType, CancellationToken cancellationToken = default)
    {
        await _context.OrganizationUnitTypes.AddAsync(organizationUnitType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(OrganizationUnitType organizationUnitType, CancellationToken cancellationToken = default)
    {
        _context.OrganizationUnitTypes.Update(organizationUnitType);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(OrganizationUnitType organizationUnitType, CancellationToken cancellationToken = default)
    {
        _context.OrganizationUnitTypes.Remove(organizationUnitType);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnitTypes
            .AnyAsync(t => t.Name == name, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<OrganizationUnitType?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnitTypes
            .FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
    }
}
