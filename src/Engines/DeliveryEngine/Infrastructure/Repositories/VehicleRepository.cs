using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.DeliveryEngine.Domain.Vehicles;
using Eleraki.DeliveryEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.DeliveryEngine.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly DeliveryDbContext _context;

    public VehicleRepository(DeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetByIdAsync(VehicleId id, CancellationToken cancellationToken = default)
    {
        return await _context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        await _context.Vehicles.AddAsync(vehicle, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
