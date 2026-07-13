using Eleraki.SharedKernel.Identity;

namespace Eleraki.PurchasingEngine.Domain.ValueObjects;

public sealed record VendorId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static VendorId New() => new(Guid.NewGuid());
    public static VendorId From(Guid value) => new(value);
}
