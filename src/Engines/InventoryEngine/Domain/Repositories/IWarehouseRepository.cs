using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.ValueObjects;

namespace Eleraki.InventoryEngine.Domain.Repositories;

public interface IWarehouseRepository
{
    Task<Warehouse?> GetByIdAsync(WarehouseId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
    Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
    Task DeleteAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
