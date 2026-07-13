using Eleraki.HREngine.Domain;

namespace Eleraki.HREngine.Domain.Repositories;

public interface IPositionRepository
{
    Task<Position?> GetByIdAsync(PositionId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Position>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Position position, CancellationToken cancellationToken = default);
    Task UpdateAsync(Position position, CancellationToken cancellationToken = default);
    Task DeleteAsync(Position position, CancellationToken cancellationToken = default);
}
