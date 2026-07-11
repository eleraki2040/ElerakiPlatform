namespace Eleraki.AuthorizationEngine.Domain.Repositories;

/// <summary>
/// Repository interface for Permission aggregate.
/// </summary>
public interface IPermissionRepository
{
    /// <summary>
    /// Gets a permission by its ID.
    /// </summary>
    Task<Permission?> GetByIdAsync(PermissionId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all permissions.
    /// </summary>
    Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new permission.
    /// </summary>
    Task AddAsync(Permission permission, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing permission.
    /// </summary>
    Task UpdateAsync(Permission permission, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a permission.
    /// </summary>
    Task DeleteAsync(Permission permission, CancellationToken cancellationToken = default);
}
