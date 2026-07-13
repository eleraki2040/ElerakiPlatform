using Eleraki.OrganizationEngine.Domain.Organizations;
using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.OrganizationEngine.Domain.ValueObjects;
using Eleraki.OrganizationEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.OrganizationEngine.Infrastructure.Repositories;

/// <summary>
/// Entity Framework Core implementation of IOrganizationRepository.
/// </summary>
public class OrganizationRepository : IOrganizationRepository
{
    private readonly OrganizationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationRepository"/> class.
    /// </summary>
    /// <param name="context">The Organization DbContext.</param>
    public OrganizationRepository(OrganizationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Organization?> GetByIdAsync(OrganizationId id, CancellationToken cancellationToken = default)
    {
        return await _context.Organizations.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Organizations.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        await _context.Organizations.AddAsync(organization, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        _context.Organizations.Update(organization);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        _context.Organizations.Remove(organization);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Organizations.AnyAsync(o => o.Code == OrganizationCode.Create(code), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Organization?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Organizations.FirstOrDefaultAsync(o => o.Code == OrganizationCode.Create(code), cancellationToken);
    }
}
