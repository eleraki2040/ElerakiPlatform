using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.InventoryEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.InventoryEngine.Infrastructure.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly InventoryDbContext _context;

    public WarehouseRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<Warehouse?> GetByIdAsync(WarehouseId id, CancellationToken cancellationToken = default)
    {
        return await _context.Warehouses.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Warehouses.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
    {
        await _context.Warehouses.AddAsync(warehouse, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
    {
        _context.Warehouses.Update(warehouse);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
    {
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Warehouses.AnyAsync(w => w.Code == code, cancellationToken);
    }
}
