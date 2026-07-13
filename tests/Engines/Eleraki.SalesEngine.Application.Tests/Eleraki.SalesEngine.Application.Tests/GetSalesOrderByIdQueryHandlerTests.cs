using Eleraki.SalesEngine.Application.DTOs;
using Eleraki.SalesEngine.Application.Queries;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SalesEngine.Domain.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;
using System.Linq;
using Xunit;

namespace Eleraki.SalesEngine.Application.Tests;

public class GetSalesOrderByIdQueryHandlerTests
{
    private readonly Mock<ISalesOrderRepository> _salesOrderRepositoryMock;
    private readonly GetSalesOrderByIdQueryHandler _handler;

    public GetSalesOrderByIdQueryHandlerTests()
    {
        _salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
        _handler = new GetSalesOrderByIdQueryHandler(_salesOrderRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Null_When_SalesOrder_Not_Found()
    {
        var salesOrderId = SalesOrderId.New();
        _salesOrderRepositoryMock.Setup(r => r.GetByIdAsync(salesOrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((SalesOrder?)null);

        var query = new GetSalesOrderByIdQuery(salesOrderId.Value);
        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_Should_Return_Dto_With_Correct_Properties_When_Found()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        var unitPrice = Money.Create(10m, "USD");
        var line = SalesOrderLine.Create(order.Id, "Widget", 2, unitPrice);
        order.AddLine(line);

        _salesOrderRepositoryMock.Setup(r => r.GetByIdAsync(order.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var query = new GetSalesOrderByIdQuery(order.Id.Value);
        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(order.Id.Value);
        result.OrderNumber.Should().Be("SO-001");
        result.CustomerId.Should().Be(customerId.Value);
        result.CustomerName.Should().Be("Eleraki");
        result.Status.Should().Be("Draft");
        result.TotalAmount.Should().Be(20m);
        result.Lines.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_Should_Map_Line_Properties_Correctly()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        var unitPrice = Money.Create(7.5m, "USD");
        var line = SalesOrderLine.Create(order.Id, "Gadget", 4, unitPrice);
        order.AddLine(line);

        _salesOrderRepositoryMock.Setup(r => r.GetByIdAsync(order.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var query = new GetSalesOrderByIdQuery(order.Id.Value);
        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        var dtoLine = result!.Lines.First();
        dtoLine.Id.Should().Be(line.Id.Value);
        dtoLine.ProductName.Should().Be("Gadget");
        dtoLine.Quantity.Should().Be(4);
        dtoLine.UnitPrice.Should().Be(7.5m);
        dtoLine.TotalPrice.Should().Be(30m);
    }

    [Fact]
    public async Task Handle_Should_Return_Empty_Lines_When_Order_Has_No_Lines()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        _salesOrderRepositoryMock.Setup(r => r.GetByIdAsync(order.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var query = new GetSalesOrderByIdQuery(order.Id.Value);
        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Lines.Should().BeEmpty();
    }
}
