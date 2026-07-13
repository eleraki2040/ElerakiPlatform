using Eleraki.SharedKernel.Identity;

namespace Eleraki.InventoryEngine.Domain.ValueObjects;

public sealed record WarehouseId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static WarehouseId New() => new(Guid.NewGuid());
    public static WarehouseId From(Guid value) => new(value);
}
