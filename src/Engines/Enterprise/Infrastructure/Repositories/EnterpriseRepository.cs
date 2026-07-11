using Eleraki.Enterprise.Domain;
using Eleraki.Enterprise.Domain.Repositories;
using Eleraki.Enterprise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.Enterprise.Infrastructure.Repositories;

/// <summary>
/// Entity Framework Core implementation of IEnterpriseRepository.
/// </summary>
public class EnterpriseRepository : IEnterpriseRepository
{
    private readonly EnterpriseDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnterpriseRepository"/> class.
    /// </summary>
    /// <param name="context">The Enterprise DbContext.</param>
    public EnterpriseRepository(EnterpriseDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Eleraki.Enterprise.Domain.Enterprise?> GetByIdAsync(Eleraki.Enterprise.Domain.EnterpriseId id, CancellationToken cancellationToken = default)
    {
        return await _context.Enterprises.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Eleraki.Enterprise.Domain.Enterprise>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Enterprises.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(Eleraki.Enterprise.Domain.Enterprise enterprise, CancellationToken cancellationToken = default)
    {
        await _context.Enterprises.AddAsync(enterprise, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Eleraki.Enterprise.Domain.Enterprise enterprise, CancellationToken cancellationToken = default)
    {
        _context.Enterprises.Update(enterprise);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Eleraki.Enterprise.Domain.Enterprise enterprise, CancellationToken cancellationToken = default)
    {
        _context.Enterprises.Remove(enterprise);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Enterprises.AsEnumerable().Any(e => e.Code.Value == code));
    }

    /// <inheritdoc/>
    public Task<Eleraki.Enterprise.Domain.Enterprise?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Enterprises.AsEnumerable().FirstOrDefault(e => e.Code.Value == code));
    }
}
