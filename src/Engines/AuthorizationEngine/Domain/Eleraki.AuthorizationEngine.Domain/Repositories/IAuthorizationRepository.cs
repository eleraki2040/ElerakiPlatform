namespace Eleraki.AuthorizationEngine.Domain.Repositories;

using Permission = Eleraki.AuthorizationEngine.Domain.Permission;
using Role = Eleraki.AuthorizationEngine.Domain.Role;

public interface IAuthorizationRepository
{
    Task<Permission?> GetPermissionByIdAsync(PermissionId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permission>> GetAllPermissionsAsync(CancellationToken cancellationToken = default);
    Task AddPermissionAsync(Permission permission, CancellationToken cancellationToken = default);
    Task UpdatePermissionAsync(Permission permission, CancellationToken cancellationToken = default);
    Task DeletePermissionAsync(Permission permission, CancellationToken cancellationToken = default);

    Task<Role?> GetRoleByIdAsync(RoleId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task AddRoleAsync(Role role, CancellationToken cancellationToken = default);
    Task UpdateRoleAsync(Role role, CancellationToken cancellationToken = default);
    Task DeleteRoleAsync(Role role, CancellationToken cancellationToken = default);
}
