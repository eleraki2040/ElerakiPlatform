using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.PurchasingEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.PurchasingEngine.Infrastructure.Repositories;

public class VendorRepository : IVendorRepository
{
    private readonly PurchasingDbContext _context;

    public VendorRepository(PurchasingDbContext context)
    {
        _context = context;
    }

    public async Task<Vendor?> GetByIdAsync(VendorId id, CancellationToken cancellationToken = default)
    {
        return await _context.Vendors
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Vendor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Vendors.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Vendor vendor, CancellationToken cancellationToken = default)
    {
        await _context.Vendors.AddAsync(vendor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Vendor vendor, CancellationToken cancellationToken = default)
    {
        _context.Vendors.Update(vendor);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Vendor vendor, CancellationToken cancellationToken = default)
    {
        _context.Vendors.Remove(vendor);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
