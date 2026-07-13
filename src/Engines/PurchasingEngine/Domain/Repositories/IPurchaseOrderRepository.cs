using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.ValueObjects;

namespace Eleraki.PurchasingEngine.Domain.Repositories;

public interface IPurchaseOrderRepository
{
    Task<PurchaseOrder?> GetByIdAsync(PurchaseOrderId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PurchaseOrder>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PurchaseOrder>> GetByVendorIdAsync(VendorId vendorId, CancellationToken cancellationToken = default);
    Task AddAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default);
    Task UpdateAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default);
    Task DeleteAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default);
}
