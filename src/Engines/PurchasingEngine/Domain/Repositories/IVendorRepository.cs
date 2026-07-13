using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.ValueObjects;

namespace Eleraki.PurchasingEngine.Domain.Repositories;

public interface IVendorRepository
{
    Task<Vendor?> GetByIdAsync(VendorId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Vendor>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Vendor vendor, CancellationToken cancellationToken = default);
    Task UpdateAsync(Vendor vendor, CancellationToken cancellationToken = default);
    Task DeleteAsync(Vendor vendor, CancellationToken cancellationToken = default);
}
