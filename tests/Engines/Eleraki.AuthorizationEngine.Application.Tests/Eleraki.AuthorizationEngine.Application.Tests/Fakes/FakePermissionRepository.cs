using Eleraki.AuthorizationEngine.Domain;
using Eleraki.AuthorizationEngine.Domain.Repositories;

namespace Eleraki.AuthorizationEngine.Application.Tests.Fakes;

public class FakePermissionRepository : IPermissionRepository, IAuthorizationRepository
{
    private readonly Dictionary<PermissionId, Permission> _permissions = new();
    private readonly Dictionary<RoleId, Role> _roles = new();

    public Task<Permission?> GetByIdAsync(PermissionId id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_permissions.GetValueOrDefault(id));
    }

    public Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<Permission>>(_permissions.Values.ToList().AsReadOnly());
    }

    public Task AddAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        _permissions[permission.Id] = permission;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        _permissions[permission.Id] = permission;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        _permissions.Remove(permission.Id);
        return Task.CompletedTask;
    }

    public Task<Permission?> GetPermissionByIdAsync(PermissionId id, CancellationToken cancellationToken = default)
    {
        return GetByIdAsync(id, cancellationToken);
    }

    public Task<IReadOnlyList<Permission>> GetAllPermissionsAsync(CancellationToken cancellationToken = default)
    {
        return GetAllAsync(cancellationToken);
    }

    public Task AddPermissionAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        return AddAsync(permission, cancellationToken);
    }

    public Task UpdatePermissionAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        return UpdateAsync(permission, cancellationToken);
    }

    public Task DeletePermissionAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(permission, cancellationToken);
    }

    public Task<Role?> GetRoleByIdAsync(RoleId id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_roles.GetValueOrDefault(id));
    }

    public Task<IReadOnlyList<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<Role>>(_roles.Values.ToList().AsReadOnly());
    }

    public Task AddRoleAsync(Role role, CancellationToken cancellationToken = default)
    {
        _roles[role.Id] = role;
        return Task.CompletedTask;
    }

    public Task UpdateRoleAsync(Role role, CancellationToken cancellationToken = default)
    {
        _roles[role.Id] = role;
        return Task.CompletedTask;
    }

    public Task DeleteRoleAsync(Role role, CancellationToken cancellationToken = default)
    {
        _roles.Remove(role.Id);
        return Task.CompletedTask;
    }
}
