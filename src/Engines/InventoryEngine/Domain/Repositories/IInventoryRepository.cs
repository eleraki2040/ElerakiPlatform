using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.ValueObjects;

namespace Eleraki.InventoryEngine.Domain.Repositories;

public interface IInventoryRepository
{
    Task<InventoryItem?> GetByIdAsync(InventoryItemId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InventoryItem>> GetLowStockAsync(int threshold, CancellationToken cancellationToken = default);
    Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default);
    Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken = default);
    Task DeleteAsync(InventoryItem item, CancellationToken cancellationToken = default);
    Task<bool> ExistsBySkuAsync(Sku sku, CancellationToken cancellationToken = default);
}
