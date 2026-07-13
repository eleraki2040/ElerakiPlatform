using Eleraki.SharedKernel.Identity;

namespace Eleraki.InventoryEngine.Domain.ValueObjects;

public sealed record StockMovementId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static StockMovementId New() => new(Guid.NewGuid());
    public static StockMovementId From(Guid value) => new(value);
}
