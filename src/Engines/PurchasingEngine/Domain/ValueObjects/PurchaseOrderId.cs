using Eleraki.SharedKernel.Identity;

namespace Eleraki.PurchasingEngine.Domain.ValueObjects;

public sealed record PurchaseOrderId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static PurchaseOrderId New() => new(Guid.NewGuid());
    public static PurchaseOrderId From(Guid value) => new(value);
}
