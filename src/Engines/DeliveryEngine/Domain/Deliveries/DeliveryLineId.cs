using Eleraki.SharedKernel.Identity;

namespace Eleraki.DeliveryEngine.Domain.Deliveries;

public sealed record DeliveryLineId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static DeliveryLineId New() => new(Guid.NewGuid());
    public static DeliveryLineId From(Guid value) => new(value);
}
