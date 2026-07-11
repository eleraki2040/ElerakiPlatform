namespace Eleraki.AuthorizationEngine.Domain.Repositories;

/// <summary>
/// Repository interface for Role aggregate.
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Gets a role by its ID.
    /// </summary>
    Task<Role?> GetByIdAsync(RoleId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all roles.
    /// </summary>
    Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new role.
    /// </summary>
    Task AddAsync(Role role, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    Task UpdateAsync(Role role, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a role.
    /// </summary>
    Task DeleteAsync(Role role, CancellationToken cancellationToken = default);
}
