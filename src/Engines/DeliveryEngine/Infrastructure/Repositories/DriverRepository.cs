using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.DeliveryEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.DeliveryEngine.Infrastructure.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly DeliveryDbContext _context;

    public DriverRepository(DeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Driver?> GetByIdAsync(DriverId id, CancellationToken cancellationToken = default)
    {
        return await _context.Drivers
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task AddAsync(Driver driver, CancellationToken cancellationToken = default)
    {
        await _context.Drivers.AddAsync(driver, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
