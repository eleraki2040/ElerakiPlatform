using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.DeliveryEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eleraki.DeliveryEngine.Infrastructure.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly DeliveryDbContext _context;

    public DeliveryRepository(DeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Delivery?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Deliveries
            .Include(d => d.Lines)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<Delivery?> GetByTrackingNumberAsync(TrackingNumber trackingNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Deliveries
            .Include(d => d.Lines)
            .FirstOrDefaultAsync(d => d.TrackingNumber.Value == trackingNumber.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Delivery>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Deliveries
            .Include(d => d.Lines)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Delivery delivery, CancellationToken cancellationToken = default)
    {
        await _context.Deliveries.AddAsync(delivery, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Delivery delivery, CancellationToken cancellationToken = default)
    {
        _context.Deliveries.Update(delivery);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Delivery delivery, CancellationToken cancellationToken = default)
    {
        _context.Deliveries.Remove(delivery);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
