using Eleraki.OrganizationEngine.Domain.Organizations;
using Eleraki.OrganizationEngine.Domain.Identity;

namespace Eleraki.OrganizationEngine.Domain.Repositories;

/// <summary>
/// Repository interface for Organization aggregate.
/// </summary>
public interface IOrganizationRepository
{
    /// <summary>
    /// Gets an organization by its ID.
    /// </summary>
    Task<Organization?> GetByIdAsync(OrganizationId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all organizations.
    /// </summary>
    Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new organization.
    /// </summary>
    Task AddAsync(Organization organization, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing organization.
    /// </summary>
    Task UpdateAsync(Organization organization, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an organization.
    /// </summary>
    Task DeleteAsync(Organization organization, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if an organization with the specified code exists.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an organization by its code.
    /// </summary>
    Task<Organization?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
}