using Eleraki.DeliveryEngine.Domain.Events;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.DeliveryEngine.Domain.Vehicles;

public sealed class Vehicle : AggregateRoot<VehicleId>
{
    public string LicensePlate { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public int Capacity { get; private set; }
    public VehicleStatus Status { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Vehicle(VehicleId id) : base(id)
    {
        LicensePlate = default!;
        Model = default!;
    }

    public static Vehicle Create(string licensePlate, string model, int capacity)
    {
        Guard.NotNullOrEmpty(licensePlate, nameof(licensePlate));
        Guard.NotNullOrEmpty(model, nameof(model));
        Guard.Ensure(capacity > 0, "Capacity must be greater than zero.");

        var vehicle = new Vehicle(VehicleId.New())
        {
            LicensePlate = licensePlate,
            Model = model,
            Capacity = capacity,
            Status = VehicleStatus.Available,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        vehicle.RaiseDomainEvent(new VehicleRegisteredDomainEvent(vehicle.Id, Guid.NewGuid(), Clock.UtcNow));

        return vehicle;
    }

    public void SetStatus(VehicleStatus status)
    {
        Status = status;
        ModifiedOn = Clock.UtcNow;
    }
}

public enum VehicleStatus
{
    Available = 1,
    InUse = 2,
    Maintenance = 3,
    OutOfService = 4
}
