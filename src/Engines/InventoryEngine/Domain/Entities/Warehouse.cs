using Eleraki.InventoryEngine.Domain.Events;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.InventoryEngine.Domain.Entities;

public sealed class Warehouse : AggregateRoot<WarehouseId>
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Address { get; private set; }
    public WarehouseStatus Status { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Warehouse(WarehouseId id) : base(id)
    {
    }

    public static Warehouse Create(string name, string code, string? address = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(code, nameof(code));

        var warehouse = new Warehouse(WarehouseId.New())
        {
            Name = name,
            Code = code.Trim().ToUpperInvariant(),
            Address = address,
            Status = WarehouseStatus.Active,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        warehouse.RaiseDomainEvent(new WarehouseCreatedDomainEvent(warehouse.Id, Guid.NewGuid(), Clock.UtcNow));

        return warehouse;
    }

    public void Update(string name, string code, string? address = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(code, nameof(code));

        Name = name;
        Code = code.Trim().ToUpperInvariant();
        Address = address;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new WarehouseUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Activate()
    {
        if (Status == WarehouseStatus.Active)
            return;

        Status = WarehouseStatus.Active;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new WarehouseActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Deactivate()
    {
        if (Status == WarehouseStatus.Inactive)
            return;

        Status = WarehouseStatus.Inactive;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new WarehouseDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum WarehouseStatus
{
    Active = 1,
    Inactive = 2
}
