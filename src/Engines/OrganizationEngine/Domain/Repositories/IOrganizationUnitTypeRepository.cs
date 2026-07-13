using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.OrganizationUnitTypes;

namespace Eleraki.OrganizationEngine.Domain.Repositories;

/// <summary>
/// Repository interface for OrganizationUnitType aggregate.
/// </summary>
public interface IOrganizationUnitTypeRepository
{
    /// <summary>
    /// Gets an organization unit type by its ID.
    /// </summary>
    Task<OrganizationUnitType?> GetByIdAsync(OrganizationUnitTypeId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all organization unit types.
    /// </summary>
    Task<IReadOnlyList<OrganizationUnitType>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active organization unit types.
    /// </summary>
    Task<IReadOnlyList<OrganizationUnitType>> GetActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new organization unit type.
    /// </summary>
    Task AddAsync(OrganizationUnitType organizationUnitType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing organization unit type.
    /// </summary>
    Task UpdateAsync(OrganizationUnitType organizationUnitType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an organization unit type.
    /// </summary>
    Task DeleteAsync(OrganizationUnitType organizationUnitType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if an organization unit type with the specified name exists.
    /// </summary>
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an organization unit type by its name.
    /// </summary>
    Task<OrganizationUnitType?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
