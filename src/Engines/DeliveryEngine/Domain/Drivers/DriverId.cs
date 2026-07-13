using Eleraki.SharedKernel.Identity;

namespace Eleraki.DeliveryEngine.Domain.Drivers;

public sealed record DriverId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static DriverId New() => new(Guid.NewGuid());
    public static DriverId From(Guid value) => new(value);
}
