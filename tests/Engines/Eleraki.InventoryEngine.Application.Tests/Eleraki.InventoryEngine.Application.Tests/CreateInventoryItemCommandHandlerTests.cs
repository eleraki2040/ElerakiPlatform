using Eleraki.InventoryEngine.Application.Commands;
using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;
using Moq;

namespace Eleraki.InventoryEngine.Application.Tests;

public class CreateInventoryItemCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Success_With_ItemId()
    {
        var repository = new Mock<IInventoryRepository>();
        var handler = new CreateInventoryItemCommandHandler(repository.Object);
        var command = new CreateInventoryItemCommand("SKU-001", "Test Item", 10, Guid.NewGuid());

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Handle_Should_Add_Item_To_Repository()
    {
        var repository = new Mock<IInventoryRepository>();
        var handler = new CreateInventoryItemCommandHandler(repository.Object);
        var warehouseId = Guid.NewGuid();
        var command = new CreateInventoryItemCommand("SKU-001", "Test Item", 10, warehouseId);

        var result = await handler.Handle(command, CancellationToken.None);

        repository.Verify(r => r.AddAsync(It.Is<InventoryItem>(item =>
            item.Sku.Value == "SKU-001" &&
            item.Name == "Test Item" &&
            item.Quantity.Value == 10 &&
            item.WarehouseId == warehouseId &&
            item.Status == InventoryItemStatus.Active
        ), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Quantity_Is_Negative()
    {
        var repository = new Mock<IInventoryRepository>();
        var handler = new CreateInventoryItemCommandHandler(repository.Object);
        var command = new CreateInventoryItemCommand("SKU-001", "Test Item", -1, Guid.NewGuid());

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("Validation");
    }

    [Fact]
    public async Task Handle_Should_Not_Add_Item_When_Quantity_Is_Negative()
    {
        var repository = new Mock<IInventoryRepository>();
        var handler = new CreateInventoryItemCommandHandler(repository.Object);
        var command = new CreateInventoryItemCommand("SKU-001", "Test Item", -5, Guid.NewGuid());

        var result = await handler.Handle(command, CancellationToken.None);

        repository.Verify(r => r.AddAsync(It.IsAny<InventoryItem>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_Include_Description_When_Provided()
    {
        var repository = new Mock<IInventoryRepository>();
        var handler = new CreateInventoryItemCommandHandler(repository.Object);
        var command = new CreateInventoryItemCommand("SKU-001", "Test Item", 10, Guid.NewGuid(), "A description");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        repository.Verify(r => r.AddAsync(It.Is<InventoryItem>(item => item.Description == "A description"), It.IsAny<CancellationToken>()), Times.Once);
    }
}
