using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Events;

namespace Eleraki.InventoryEngine.Domain.Events;

public sealed record InventoryItemCreatedDomainEvent(InventoryItemId InventoryItemId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record InventoryItemUpdatedDomainEvent(InventoryItemId InventoryItemId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record StockUpdatedDomainEvent(InventoryItemId InventoryItemId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record StockAdjustedDomainEvent(InventoryItemId InventoryItemId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record StockTransferredDomainEvent(InventoryItemId InventoryItemId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record StockMovementRecordedDomainEvent(StockMovementId StockMovementId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record WarehouseCreatedDomainEvent(WarehouseId WarehouseId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record WarehouseUpdatedDomainEvent(WarehouseId WarehouseId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record WarehouseActivatedDomainEvent(WarehouseId WarehouseId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record WarehouseDeactivatedDomainEvent(WarehouseId WarehouseId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
