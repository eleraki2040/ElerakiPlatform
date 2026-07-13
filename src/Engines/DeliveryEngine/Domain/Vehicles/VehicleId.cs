using Eleraki.SharedKernel.Identity;

namespace Eleraki.DeliveryEngine.Domain.Vehicles;

public sealed record VehicleId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static VehicleId New() => new(Guid.NewGuid());
    public static VehicleId From(Guid value) => new(value);
}
