using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.OrganizationUnits;

namespace Eleraki.OrganizationEngine.Domain.Repositories;

/// <summary>
/// Repository interface for OrganizationUnit aggregate.
/// </summary>
public interface IOrganizationUnitRepository
{
    /// <summary>
    /// Gets an organization unit by its ID.
    /// </summary>
    Task<OrganizationUnit?> GetByIdAsync(OrganizationUnitId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all organization units for an organization.
    /// </summary>
    Task<IReadOnlyList<OrganizationUnit>> GetAllByOrganizationIdAsync(OrganizationId organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the root organization unit for an organization.
    /// </summary>
    Task<OrganizationUnit?> GetRootAsync(OrganizationId organizationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets children of a parent organization unit.
    /// </summary>
    Task<IReadOnlyList<OrganizationUnit>> GetChildrenAsync(OrganizationUnitId parentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new organization unit.
    /// </summary>
    Task AddAsync(OrganizationUnit organizationUnit, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing organization unit.
    /// </summary>
    Task UpdateAsync(OrganizationUnit organizationUnit, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an organization unit.
    /// </summary>
    Task DeleteAsync(OrganizationUnit organizationUnit, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if an organization unit with the specified code exists within an organization.
    /// </summary>
    Task<bool> ExistsByCodeAsync(OrganizationId organizationId, string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an organization unit by its code within an organization.
    /// </summary>
    Task<OrganizationUnit?> GetByCodeAsync(OrganizationId organizationId, string code, CancellationToken cancellationToken = default);
}
