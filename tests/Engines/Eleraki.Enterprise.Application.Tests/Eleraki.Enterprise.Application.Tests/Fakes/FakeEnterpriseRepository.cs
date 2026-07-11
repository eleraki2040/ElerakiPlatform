using Eleraki.Enterprise.Domain.Repositories;

namespace Eleraki.Enterprise.Application.Tests.Fakes;

/// <summary>
/// In-memory fake implementation of IEnterpriseRepository for testing.
/// </summary>
public class FakeEnterpriseRepository : IEnterpriseRepository
{
    private readonly Dictionary<global::Eleraki.Enterprise.Domain.EnterpriseId, global::Eleraki.Enterprise.Domain.Enterprise> _store = new();

    /// <inheritdoc/>
    public Task<global::Eleraki.Enterprise.Domain.Enterprise?> GetByIdAsync(global::Eleraki.Enterprise.Domain.EnterpriseId id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.GetValueOrDefault(id));
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<global::Eleraki.Enterprise.Domain.Enterprise>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<global::Eleraki.Enterprise.Domain.Enterprise>>(_store.Values.ToList().AsReadOnly());
    }

    /// <inheritdoc/>
    public Task AddAsync(global::Eleraki.Enterprise.Domain.Enterprise enterprise, CancellationToken cancellationToken = default)
    {
        _store[enterprise.Id] = enterprise;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task UpdateAsync(global::Eleraki.Enterprise.Domain.Enterprise enterprise, CancellationToken cancellationToken = default)
    {
        _store[enterprise.Id] = enterprise;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task DeleteAsync(global::Eleraki.Enterprise.Domain.Enterprise enterprise, CancellationToken cancellationToken = default)
    {
        _store.Remove(enterprise.Id);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.Values.Any(e => e.Code.Value.Equals(code, StringComparison.OrdinalIgnoreCase)));
    }

    /// <inheritdoc/>
    public Task<global::Eleraki.Enterprise.Domain.Enterprise?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.Values.FirstOrDefault(e => e.Code.Value.Equals(code, StringComparison.OrdinalIgnoreCase)));
    }
}
