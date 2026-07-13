using Eleraki.InventoryEngine.Domain.Events;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.InventoryEngine.Domain.Entities;

public sealed class InventoryItem : AggregateRoot<InventoryItemId>
{
    public Sku Sku { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Quantity Quantity { get; private set; }
    public Location? Location { get; private set; }
    public Guid WarehouseId { get; private set; }
    public InventoryItemStatus Status { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private InventoryItem(InventoryItemId id) : base(id)
    {
        Quantity = default!;
    }

    public static InventoryItem Create(Sku sku, string name, int quantity, Guid warehouseId, string? description = null)
    {
        Guard.NotNull(sku, nameof(sku));
        Guard.NotNullOrEmpty(name, nameof(name));

        if (warehouseId == Guid.Empty)
            throw new ArgumentException("Warehouse ID cannot be empty.", nameof(warehouseId));

        var item = new InventoryItem(InventoryItemId.New())
        {
            Sku = sku,
            Name = name,
            Description = description,
            Quantity = Quantity.Create(quantity),
            WarehouseId = warehouseId,
            Status = InventoryItemStatus.Active,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        item.RaiseDomainEvent(new InventoryItemCreatedDomainEvent(item.Id, Guid.NewGuid(), Clock.UtcNow));

        return item;
    }

    public void Update(string name, string? description)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        Name = name;
        Description = description;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new InventoryItemUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void UpdateStock(int quantity)
    {
        Quantity = Quantity.Create(quantity);
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new StockUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void AdjustStock(int delta)
    {
        if (Quantity.Value + delta < 0)
            throw new InvalidOperationException("Stock cannot be adjusted below zero.");

        Quantity = Quantity.Create(Quantity.Value + delta);
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new StockAdjustedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void UpdateLocation(Location location)
    {
        Location = location;
        ModifiedOn = Clock.UtcNow;
    }

    public void Transfer(Guid warehouseId)
    {
        if (warehouseId == Guid.Empty)
            throw new ArgumentException("Warehouse ID cannot be empty.", nameof(warehouseId));

        WarehouseId = warehouseId;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new StockTransferredDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Activate()
    {
        if (Status == InventoryItemStatus.Active)
            return;

        Status = InventoryItemStatus.Active;
        ModifiedOn = Clock.UtcNow;
    }

    public void Deactivate()
    {
        if (Status == InventoryItemStatus.Inactive)
            return;

        Status = InventoryItemStatus.Inactive;
        ModifiedOn = Clock.UtcNow;
    }
}

public enum InventoryItemStatus
{
    Active = 1,
    Inactive = 2,
    Discontinued = 3
}
