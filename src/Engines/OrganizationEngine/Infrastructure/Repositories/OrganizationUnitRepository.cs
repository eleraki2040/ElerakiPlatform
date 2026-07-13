using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.OrganizationUnits;
using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.OrganizationEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.OrganizationEngine.Infrastructure.Repositories;

/// <summary>
/// Entity Framework Core implementation of IOrganizationUnitRepository.
/// </summary>
public class OrganizationUnitRepository : IOrganizationUnitRepository
{
    private readonly OrganizationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUnitRepository"/> class.
    /// </summary>
    /// <param name="context">The Organization DbContext.</param>
    public OrganizationUnitRepository(OrganizationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<OrganizationUnit?> GetByIdAsync(OrganizationUnitId id, CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnits.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrganizationUnit>> GetAllByOrganizationIdAsync(OrganizationId organizationId, CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnits
            .Where(ou => ou.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<OrganizationUnit?> GetRootAsync(OrganizationId organizationId, CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnits
            .FirstOrDefaultAsync(ou => ou.OrganizationId == organizationId && ou.ParentId == null, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrganizationUnit>> GetChildrenAsync(OrganizationUnitId parentId, CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnits
            .Where(ou => ou.ParentId == parentId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(OrganizationUnit organizationUnit, CancellationToken cancellationToken = default)
    {
        await _context.OrganizationUnits.AddAsync(organizationUnit, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(OrganizationUnit organizationUnit, CancellationToken cancellationToken = default)
    {
        _context.OrganizationUnits.Update(organizationUnit);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(OrganizationUnit organizationUnit, CancellationToken cancellationToken = default)
    {
        _context.OrganizationUnits.Remove(organizationUnit);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsByCodeAsync(OrganizationId organizationId, string code, CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnits
            .AnyAsync(ou => ou.OrganizationId == organizationId && ou.Code == code, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<OrganizationUnit?> GetByCodeAsync(OrganizationId organizationId, string code, CancellationToken cancellationToken = default)
    {
        return await _context.OrganizationUnits
            .FirstOrDefaultAsync(ou => ou.OrganizationId == organizationId && ou.Code == code, cancellationToken);
    }
}
