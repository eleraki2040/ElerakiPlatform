using Eleraki.SharedKernel.Identity;

namespace Eleraki.PurchasingEngine.Domain.ValueObjects;

public sealed record PurchaseOrderLineId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static PurchaseOrderLineId New() => new(Guid.NewGuid());
    public static PurchaseOrderLineId From(Guid value) => new(value);
}
