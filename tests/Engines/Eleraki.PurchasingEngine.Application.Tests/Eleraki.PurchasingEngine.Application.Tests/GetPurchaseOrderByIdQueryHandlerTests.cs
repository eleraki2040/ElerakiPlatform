using Eleraki.PurchasingEngine.Application.DTOs;
using Eleraki.PurchasingEngine.Application.Queries;
using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Eleraki.PurchasingEngine.Application.Tests;

public class GetPurchaseOrderByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenOrderExists_ShouldReturnDto()
    {
        var order = PurchaseOrder.Create(VendorId.New());
        var repositoryMock = new Mock<IPurchaseOrderRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(order.Id, It.IsAny<CancellationToken>())).ReturnsAsync(order);

        var handler = new GetPurchaseOrderByIdQueryHandler(repositoryMock.Object);
        var query = new GetPurchaseOrderByIdQuery(order.Id.Value);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(order.Id.Value);
        result.Status.Should().Be(order.Status.ToString());
    }

    [Fact]
    public async Task Handle_WhenOrderNotFound_ShouldReturnNull()
    {
        var repositoryMock = new Mock<IPurchaseOrderRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<PurchaseOrderId>(), It.IsAny<CancellationToken>())).ReturnsAsync((PurchaseOrder?)null);

        var handler = new GetPurchaseOrderByIdQueryHandler(repositoryMock.Object);
        var query = new GetPurchaseOrderByIdQuery(Guid.NewGuid());

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }
}
