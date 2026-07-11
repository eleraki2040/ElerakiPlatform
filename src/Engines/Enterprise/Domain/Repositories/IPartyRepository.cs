namespace Eleraki.Enterprise.Domain.Repositories;

/// <summary>
/// Repository interface for Party aggregate.
/// </summary>
public interface IPartyRepository
{
    /// <summary>
    /// Gets a party by its ID.
    /// </summary>
    Task<Party?> GetByIdAsync(PartyId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all parties.
    /// </summary>
    Task<IReadOnlyList<Party>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new party.
    /// </summary>
    Task AddAsync(Party party, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing party.
    /// </summary>
    Task UpdateAsync(Party party, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a party.
    /// </summary>
    Task DeleteAsync(Party party, CancellationToken cancellationToken = default);
}
