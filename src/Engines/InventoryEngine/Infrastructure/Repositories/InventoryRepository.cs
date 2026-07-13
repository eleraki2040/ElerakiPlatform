using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.InventoryEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.InventoryEngine.Infrastructure.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly InventoryDbContext _context;

    public InventoryRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<InventoryItem?> GetByIdAsync(InventoryItemId id, CancellationToken cancellationToken = default)
    {
        return await _context.InventoryItems.FirstOrDefaultAsync(i => i.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.InventoryItems.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetLowStockAsync(int threshold, CancellationToken cancellationToken = default)
    {
        return await _context.InventoryItems
            .Where(item => item.Quantity.Value <= threshold)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default)
    {
        await _context.InventoryItems.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken = default)
    {
        _context.InventoryItems.Update(item);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InventoryItem item, CancellationToken cancellationToken = default)
    {
        _context.InventoryItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsBySkuAsync(Sku sku, CancellationToken cancellationToken = default)
    {
        return await _context.InventoryItems.AnyAsync(item => item.Sku.Value == sku.Value, cancellationToken);
    }
}
