using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.ValueObjects;

namespace Eleraki.InventoryEngine.Domain.Tests;

public class InventoryItemTests
{
    [Fact]
    public void Create_Should_Return_Active_Item_With_Correct_Values()
    {
        var sku = Sku.Create("SKU-001");
        var warehouseId = Guid.NewGuid();

        var item = InventoryItem.Create(sku, "Test Item", 10, warehouseId, "A test item");

        item.Should().NotBeNull();
        item.Sku.Value.Should().Be("SKU-001");
        item.Name.Should().Be("Test Item");
        item.Quantity.Value.Should().Be(10);
        item.WarehouseId.Should().Be(warehouseId);
        item.Status.Should().Be(InventoryItemStatus.Active);
        item.Description.Should().Be("A test item");
        item.CreatedOn.Should().NotBe(default);
        item.ModifiedOn.Should().NotBe(default);
    }

    [Fact]
    public void Create_Should_Raise_InventoryItemCreatedDomainEvent()
    {
        var sku = Sku.Create("SKU-001");
        var warehouseId = Guid.NewGuid();

        var item = InventoryItem.Create(sku, "Test Item", 10, warehouseId);

        item.DomainEvents.Should().Contain(e => e.GetType().Name == "InventoryItemCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_WarehouseId_Is_Empty()
    {
        var sku = Sku.Create("SKU-001");

        var act = () => InventoryItem.Create(sku, "Test Item", 10, Guid.Empty);

        act.Should().Throw<ArgumentException>().WithMessage("*Warehouse ID cannot be empty*");
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Null()
    {
        var sku = Sku.Create("SKU-001");
        var warehouseId = Guid.NewGuid();

        var act = () => InventoryItem.Create(sku, null!, 10, warehouseId);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UpdateStock_Should_Set_Quantity()
    {
        var sku = Sku.Create("SKU-001");
        var warehouseId = Guid.NewGuid();
        var item = InventoryItem.Create(sku, "Test Item", 10, warehouseId);

        item.UpdateStock(50);

        item.Quantity.Value.Should().Be(50);
    }

    [Fact]
    public void UpdateStock_Should_Raise_StockUpdatedDomainEvent()
    {
        var sku = Sku.Create("SKU-001");
        var warehouseId = Guid.NewGuid();
        var item = InventoryItem.Create(sku, "Test Item", 10, warehouseId);

        item.UpdateStock(50);

        item.DomainEvents.Should().Contain(e => e.GetType().Name == "StockUpdatedDomainEvent");
    }

    [Fact]
    public void Deactivate_Should_Set_Status_To_Inactive()
    {
        var sku = Sku.Create("SKU-001");
        var warehouseId = Guid.NewGuid();
        var item = InventoryItem.Create(sku, "Test Item", 10, warehouseId);

        item.Deactivate();

        item.Status.Should().Be(InventoryItemStatus.Inactive);
    }

    [Fact]
    public void Deactivate_Should_Not_Raise_Event_If_Already_Inactive()
    {
        var sku = Sku.Create("SKU-001");
        var warehouseId = Guid.NewGuid();
        var item = InventoryItem.Create(sku, "Test Item", 10, warehouseId);

        item.Deactivate();
        var eventsAfterFirst = item.DomainEvents.Count;
        item.Deactivate();

        item.DomainEvents.Count.Should().Be(eventsAfterFirst);
    }

    [Fact]
    public void Activate_Should_Set_Status_To_Active()
    {
        var sku = Sku.Create("SKU-001");
        var warehouseId = Guid.NewGuid();
        var item = InventoryItem.Create(sku, "Test Item", 10, warehouseId);

        item.Deactivate();
        item.Activate();

        item.Status.Should().Be(InventoryItemStatus.Active);
    }
}
