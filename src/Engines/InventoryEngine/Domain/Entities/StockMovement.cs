using Eleraki.InventoryEngine.Domain.Events;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.InventoryEngine.Domain.Entities;

public sealed class StockMovement : AggregateRoot<StockMovementId>
{
    public Guid InventoryItemId { get; private set; }
    public MovementType MovementType { get; private set; }
    public Quantity Quantity { get; private set; }
    public string? Reference { get; private set; }
    public string? Notes { get; private set; }
    public Guid PerformedBy { get; private set; }
    public DateTime CreatedOn { get; private set; }

    private StockMovement(StockMovementId id) : base(id)
    {
        Quantity = default!;
    }

    public static StockMovement Create(Guid inventoryItemId, MovementType movementType, int quantity, string? reference = null, string? notes = null, Guid performedBy = default)
    {
        if (inventoryItemId == Guid.Empty)
            throw new ArgumentException("Inventory item ID cannot be empty.", nameof(inventoryItemId));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        if (performedBy == Guid.Empty)
            performedBy = Guid.NewGuid();

        var movement = new StockMovement(StockMovementId.New())
        {
            InventoryItemId = inventoryItemId,
            MovementType = movementType,
            Quantity = Quantity.Create(quantity),
            Reference = reference,
            Notes = notes,
            PerformedBy = performedBy,
            CreatedOn = Clock.UtcNow
        };

        movement.RaiseDomainEvent(new StockMovementRecordedDomainEvent(movement.Id, Guid.NewGuid(), Clock.UtcNow));

        return movement;
    }
}

public enum MovementType
{
    Receipt = 1,
    Adjustment = 2,
    Transfer = 3,
    Return = 4,
    Disposal = 5
}
