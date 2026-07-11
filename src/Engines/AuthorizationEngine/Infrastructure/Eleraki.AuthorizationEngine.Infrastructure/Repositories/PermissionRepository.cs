using Eleraki.AuthorizationEngine.Domain;
using Eleraki.AuthorizationEngine.Domain.Repositories;
using Eleraki.AuthorizationEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.AuthorizationEngine.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Permission aggregate.
/// </summary>
public class PermissionRepository : IPermissionRepository
{
    private readonly AuthorizationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionRepository"/> class.
    /// </summary>
    /// <param name="context">The Authorization DbContext.</param>
    public PermissionRepository(AuthorizationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Permission?> GetByIdAsync(PermissionId id, CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        await _context.Permissions.AddAsync(permission, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        _context.Permissions.Update(permission);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
