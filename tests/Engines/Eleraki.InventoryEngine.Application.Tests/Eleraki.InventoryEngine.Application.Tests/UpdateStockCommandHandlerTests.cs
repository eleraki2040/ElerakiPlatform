using Eleraki.InventoryEngine.Application.Commands;
using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;
using Moq;

namespace Eleraki.InventoryEngine.Application.Tests;

public class UpdateStockCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Success_When_Item_Exists()
    {
        var repository = new Mock<IInventoryRepository>();
        var item = InventoryItem.Create(Sku.Create("SKU-001"), "Test Item", 10, Guid.NewGuid());
        repository.Setup(r => r.GetByIdAsync(It.IsAny<InventoryItemId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);
        var handler = new UpdateStockCommandHandler(repository.Object);
        var command = new UpdateStockCommand(item.Id.Value, 50);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(item.Id.Value);
    }

    [Fact]
    public async Task Handle_Should_Update_Item_Quantity()
    {
        var repository = new Mock<IInventoryRepository>();
        var item = InventoryItem.Create(Sku.Create("SKU-001"), "Test Item", 10, Guid.NewGuid());
        repository.Setup(r => r.GetByIdAsync(It.IsAny<InventoryItemId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);
        var handler = new UpdateStockCommandHandler(repository.Object);
        var command = new UpdateStockCommand(item.Id.Value, 50);

        await handler.Handle(command, CancellationToken.None);

        item.Quantity.Value.Should().Be(50);
    }

    [Fact]
    public async Task Handle_Should_Call_Repository_UpdateAsync()
    {
        var repository = new Mock<IInventoryRepository>();
        var item = InventoryItem.Create(Sku.Create("SKU-001"), "Test Item", 10, Guid.NewGuid());
        repository.Setup(r => r.GetByIdAsync(It.IsAny<InventoryItemId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);
        var handler = new UpdateStockCommandHandler(repository.Object);
        var command = new UpdateStockCommand(item.Id.Value, 50);

        await handler.Handle(command, CancellationToken.None);

        repository.Verify(r => r.UpdateAsync(item, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Item_Not_Found()
    {
        var repository = new Mock<IInventoryRepository>();
        repository.Setup(r => r.GetByIdAsync(It.IsAny<InventoryItemId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryItem?)null);
        var handler = new UpdateStockCommandHandler(repository.Object);
        var command = new UpdateStockCommand(Guid.NewGuid(), 50);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("NotFound");
    }

    [Fact]
    public async Task Handle_Should_Not_Call_UpdateAsync_When_Item_Not_Found()
    {
        var repository = new Mock<IInventoryRepository>();
        repository.Setup(r => r.GetByIdAsync(It.IsAny<InventoryItemId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryItem?)null);
        var handler = new UpdateStockCommandHandler(repository.Object);
        var command = new UpdateStockCommand(Guid.NewGuid(), 50);

        await handler.Handle(command, CancellationToken.None);

        repository.Verify(r => r.UpdateAsync(It.IsAny<InventoryItem>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
