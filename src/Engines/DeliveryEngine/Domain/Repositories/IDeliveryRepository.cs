using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.Vehicles;

namespace Eleraki.DeliveryEngine.Domain.Repositories;

public interface IDeliveryRepository
{
    Task<Delivery?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Delivery?> GetByTrackingNumberAsync(TrackingNumber trackingNumber, CancellationToken cancellationToken = default);
    Task AddAsync(Delivery delivery, CancellationToken cancellationToken = default);
}

public interface IDriverRepository
{
    Task<Driver?> GetByIdAsync(DriverId id, CancellationToken cancellationToken = default);
    Task AddAsync(Driver driver, CancellationToken cancellationToken = default);
}

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(VehicleId id, CancellationToken cancellationToken = default);
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
}
