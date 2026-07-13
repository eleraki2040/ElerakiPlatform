using Eleraki.PurchasingEngine.Application.Commands;
using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Eleraki.PurchasingEngine.Application.Tests;

public class AddPurchaseOrderLineCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenValidRequest_ShouldAddLine()
    {
        var order = PurchaseOrder.Create(VendorId.New());
        var repositoryMock = new Mock<IPurchaseOrderRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(order.Id, It.IsAny<CancellationToken>())).ReturnsAsync(order);

        var handler = new AddPurchaseOrderLineCommandHandler(repositoryMock.Object);
        var command = new AddPurchaseOrderLineCommand(order.Id.Value, "Widget", 2, 10m, "USD");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        order.Lines.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_WhenPurchaseOrderNotFound_ShouldReturnFailure()
    {
        var repositoryMock = new Mock<IPurchaseOrderRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<PurchaseOrderId>(), It.IsAny<CancellationToken>())).ReturnsAsync((PurchaseOrder?)null);

        var handler = new AddPurchaseOrderLineCommandHandler(repositoryMock.Object);
        var command = new AddPurchaseOrderLineCommand(Guid.NewGuid(), "Widget", 1, 10m, "USD");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WhenQuantityIsZero_ShouldReturnFailure()
    {
        var order = PurchaseOrder.Create(VendorId.New());
        var repositoryMock = new Mock<IPurchaseOrderRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(order.Id, It.IsAny<CancellationToken>())).ReturnsAsync(order);

        var handler = new AddPurchaseOrderLineCommandHandler(repositoryMock.Object);
        var command = new AddPurchaseOrderLineCommand(order.Id.Value, "Widget", 0, 10m, "USD");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
}
