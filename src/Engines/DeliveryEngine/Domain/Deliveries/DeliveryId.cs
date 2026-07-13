using Eleraki.SharedKernel.Identity;

namespace Eleraki.DeliveryEngine.Domain.Deliveries;

public sealed record DeliveryId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static DeliveryId New() => new(Guid.NewGuid());
    public static DeliveryId From(Guid value) => new(value);
}
