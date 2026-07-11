using Eleraki.Enterprise.Domain;

namespace Eleraki.Enterprise.Domain.Repositories;

/// <summary>
/// Repository interface for Enterprise aggregate.
/// </summary>
public interface IEnterpriseRepository
{
    /// <summary>
    /// Gets an enterprise by its ID.
    /// </summary>
    Task<Enterprise?> GetByIdAsync(EnterpriseId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all enterprises.
    /// </summary>
    Task<IReadOnlyList<Enterprise>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new enterprise.
    /// </summary>
    Task AddAsync(Enterprise enterprise, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing enterprise.
    /// </summary>
    Task UpdateAsync(Enterprise enterprise, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an enterprise.
    /// </summary>
    Task DeleteAsync(Enterprise enterprise, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if an enterprise with the specified code exists.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an enterprise by its code.
    /// </summary>
    Task<Enterprise?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
}