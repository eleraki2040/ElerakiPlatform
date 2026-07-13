using Eleraki.SharedKernel.Identity;

namespace Eleraki.InventoryEngine.Domain.ValueObjects;

public sealed record InventoryItemId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static InventoryItemId New() => new(Guid.NewGuid());
    public static InventoryItemId From(Guid value) => new(value);
}
