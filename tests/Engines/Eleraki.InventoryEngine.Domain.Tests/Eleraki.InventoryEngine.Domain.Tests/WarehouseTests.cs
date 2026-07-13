using Eleraki.InventoryEngine.Domain.Entities;

namespace Eleraki.InventoryEngine.Domain.Tests;

public class WarehouseTests
{
    [Fact]
    public void Create_Should_Return_Active_Warehouse_With_Correct_Values()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "WH-001", "123 Main St");

        warehouse.Should().NotBeNull();
        warehouse.Name.Should().Be("Main Warehouse");
        warehouse.Code.Should().Be("WH-001");
        warehouse.Address.Should().Be("123 Main St");
        warehouse.Status.Should().Be(WarehouseStatus.Active);
        warehouse.CreatedOn.Should().NotBe(default);
        warehouse.ModifiedOn.Should().NotBe(default);
    }

    [Fact]
    public void Create_Should_Raise_WarehouseCreatedDomainEvent()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "WH-001");

        warehouse.DomainEvents.Should().Contain(e => e.GetType().Name == "WarehouseCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Trim_And_Uppercase_Code()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "  wh-001  ");

        warehouse.Code.Should().Be("WH-001");
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Null()
    {
        var act = () => Warehouse.Create(null!, "WH-001");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Code_Is_Null()
    {
        var act = () => Warehouse.Create("Main Warehouse", null!);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Update_Should_Change_Values()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "WH-001");

        warehouse.Update("Updated Warehouse", "WH-002", "456 New St");

        warehouse.Name.Should().Be("Updated Warehouse");
        warehouse.Code.Should().Be("WH-002");
        warehouse.Address.Should().Be("456 New St");
    }

    [Fact]
    public void Update_Should_Trim_And_Uppercase_Code()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "WH-001");

        warehouse.Update("Updated Warehouse", "  wh-002  ");

        warehouse.Code.Should().Be("WH-002");
    }

    [Fact]
    public void Update_Should_Raise_WarehouseUpdatedDomainEvent()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "WH-001");

        warehouse.Update("Updated Warehouse", "WH-002");

        warehouse.DomainEvents.Should().Contain(e => e.GetType().Name == "WarehouseUpdatedDomainEvent");
    }

    [Fact]
    public void Deactivate_Should_Set_Status_To_Inactive()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "WH-001");

        warehouse.Deactivate();

        warehouse.Status.Should().Be(WarehouseStatus.Inactive);
    }

    [Fact]
    public void Deactivate_Should_Raise_WarehouseDeactivatedDomainEvent()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "WH-001");

        warehouse.Deactivate();

        warehouse.DomainEvents.Should().Contain(e => e.GetType().Name == "WarehouseDeactivatedDomainEvent");
    }

    [Fact]
    public void Activate_Should_Set_Status_To_Active()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "WH-001");
        warehouse.Deactivate();

        warehouse.Activate();

        warehouse.Status.Should().Be(WarehouseStatus.Active);
    }

    [Fact]
    public void Activate_Should_Raise_WarehouseActivatedDomainEvent()
    {
        var warehouse = Warehouse.Create("Main Warehouse", "WH-001");
        warehouse.Deactivate();

        warehouse.Activate();

        warehouse.DomainEvents.Should().Contain(e => e.GetType().Name == "WarehouseActivatedDomainEvent");
    }
}
