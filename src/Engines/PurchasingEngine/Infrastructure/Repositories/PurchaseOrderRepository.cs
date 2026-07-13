using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.PurchasingEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.PurchasingEngine.Infrastructure.Repositories;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
    private readonly PurchasingDbContext _context;

    public PurchaseOrderRepository(PurchasingDbContext context)
    {
        _context = context;
    }

    public async Task<PurchaseOrder?> GetByIdAsync(PurchaseOrderId id, CancellationToken cancellationToken = default)
    {
        return await _context.PurchaseOrders
            .FirstOrDefaultAsync(po => po.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PurchaseOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.PurchaseOrders.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PurchaseOrder>> GetByVendorIdAsync(VendorId vendorId, CancellationToken cancellationToken = default)
    {
        return await _context.PurchaseOrders
            .Where(po => po.VendorId == vendorId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
    {
        await _context.PurchaseOrders.AddAsync(purchaseOrder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
    {
        _context.PurchaseOrders.Update(purchaseOrder);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
    {
        _context.PurchaseOrders.Remove(purchaseOrder);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
