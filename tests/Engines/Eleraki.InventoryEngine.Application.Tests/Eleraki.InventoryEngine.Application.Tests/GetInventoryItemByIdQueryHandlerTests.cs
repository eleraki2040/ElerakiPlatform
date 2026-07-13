using Eleraki.InventoryEngine.Application.DTOs;
using Eleraki.InventoryEngine.Application.Queries;
using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;
using Moq;

namespace Eleraki.InventoryEngine.Application.Tests;

public class GetInventoryItemByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Dto_When_Item_Exists()
    {
        var repository = new Mock<IInventoryRepository>();
        var item = InventoryItem.Create(Sku.Create("SKU-001"), "Test Item", 10, Guid.NewGuid(), "A description");
        repository.Setup(r => r.GetByIdAsync(It.IsAny<InventoryItemId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);
        var handler = new GetInventoryItemByIdQueryHandler(repository.Object);
        var query = new GetInventoryItemByIdQuery(item.Id.Value);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(item.Id.Value);
        result.Sku.Should().Be("SKU-001");
        result.Name.Should().Be("Test Item");
        result.Description.Should().Be("A description");
        result.Quantity.Should().Be(10);
        result.WarehouseId.Should().Be(item.WarehouseId);
        result.Status.Should().Be("Active");
    }

    [Fact]
    public async Task Handle_Should_Return_Null_When_Item_Not_Found()
    {
        var repository = new Mock<IInventoryRepository>();
        repository.Setup(r => r.GetByIdAsync(It.IsAny<InventoryItemId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryItem?)null);
        var handler = new GetInventoryItemByIdQueryHandler(repository.Object);
        var query = new GetInventoryItemByIdQuery(Guid.NewGuid());

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_Should_Map_Status_ToString()
    {
        var repository = new Mock<IInventoryRepository>();
        var item = InventoryItem.Create(Sku.Create("SKU-001"), "Test Item", 10, Guid.NewGuid());
        item.Deactivate();
        repository.Setup(r => r.GetByIdAsync(It.IsAny<InventoryItemId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);
        var handler = new GetInventoryItemByIdQueryHandler(repository.Object);
        var query = new GetInventoryItemByIdQuery(item.Id.Value);

        var result = await handler.Handle(query, CancellationToken.None);

        result!.Status.Should().Be("Inactive");
    }

    [Fact]
    public async Task Handle_Should_Return_Null_Location_When_Item_Has_No_Location()
    {
        var repository = new Mock<IInventoryRepository>();
        var item = InventoryItem.Create(Sku.Create("SKU-001"), "Test Item", 10, Guid.NewGuid());
        repository.Setup(r => r.GetByIdAsync(It.IsAny<InventoryItemId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);
        var handler = new GetInventoryItemByIdQueryHandler(repository.Object);
        var query = new GetInventoryItemByIdQuery(item.Id.Value);

        var result = await handler.Handle(query, CancellationToken.None);

        result!.Location.Should().BeNull();
    }
}
