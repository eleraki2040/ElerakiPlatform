using Eleraki.InventoryEngine.Domain.Entities;

namespace Eleraki.InventoryEngine.Domain.Tests;

public class StockMovementTests
{
    [Fact]
    public void Create_Should_Return_StockMovement_With_Correct_Values()
    {
        var inventoryItemId = Guid.NewGuid();

        var movement = StockMovement.Create(inventoryItemId, MovementType.Receipt, 20, "PO-001", "Received from supplier");

        movement.Should().NotBeNull();
        movement.InventoryItemId.Should().Be(inventoryItemId);
        movement.MovementType.Should().Be(MovementType.Receipt);
        movement.Quantity.Value.Should().Be(20);
        movement.Reference.Should().Be("PO-001");
        movement.Notes.Should().Be("Received from supplier");
        movement.CreatedOn.Should().NotBe(default);
    }

    [Fact]
    public void Create_Should_Raise_StockMovementRecordedDomainEvent()
    {
        var inventoryItemId = Guid.NewGuid();

        var movement = StockMovement.Create(inventoryItemId, MovementType.Receipt, 20);

        movement.DomainEvents.Should().Contain(e => e.GetType().Name == "StockMovementRecordedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_InventoryItemId_Is_Empty()
    {
        var act = () => StockMovement.Create(Guid.Empty, MovementType.Receipt, 20);

        act.Should().Throw<ArgumentException>().WithMessage("*Inventory item ID cannot be empty*");
    }

    [Fact]
    public void Create_Should_Throw_When_Quantity_Is_Zero()
    {
        var inventoryItemId = Guid.NewGuid();

        var act = () => StockMovement.Create(inventoryItemId, MovementType.Receipt, 0);

        act.Should().Throw<ArgumentException>().WithMessage("*Quantity must be greater than zero*");
    }

    [Fact]
    public void Create_Should_Throw_When_Quantity_Is_Negative()
    {
        var inventoryItemId = Guid.NewGuid();

        var act = () => StockMovement.Create(inventoryItemId, MovementType.Receipt, -5);

        act.Should().Throw<ArgumentException>().WithMessage("*Quantity must be greater than zero*");
    }

    [Fact]
    public void Create_Should_Assign_New_PerformedBy_When_Not_Provided()
    {
        var inventoryItemId = Guid.NewGuid();

        var movement = StockMovement.Create(inventoryItemId, MovementType.Receipt, 20);

        movement.PerformedBy.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Create_Should_Use_Provided_PerformedBy()
    {
        var inventoryItemId = Guid.NewGuid();
        var performedBy = Guid.NewGuid();

        var movement = StockMovement.Create(inventoryItemId, MovementType.Receipt, 20, performedBy: performedBy);

        movement.PerformedBy.Should().Be(performedBy);
    }

    [Theory]
    [InlineData(MovementType.Receipt)]
    [InlineData(MovementType.Adjustment)]
    [InlineData(MovementType.Transfer)]
    [InlineData(MovementType.Return)]
    [InlineData(MovementType.Disposal)]
    public void Create_Should_Accept_All_Movement_Types(MovementType movementType)
    {
        var inventoryItemId = Guid.NewGuid();

        var movement = StockMovement.Create(inventoryItemId, movementType, 20);

        movement.MovementType.Should().Be(movementType);
    }
}
