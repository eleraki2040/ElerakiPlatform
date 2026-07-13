using Eleraki.SalesEngine.Application.Commands;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SalesEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Eleraki.SalesEngine.Application.Tests;

public class AddSalesOrderLineCommandHandlerTests
{
    private readonly Mock<ISalesOrderRepository> _salesOrderRepositoryMock;
    private readonly AddSalesOrderLineCommandHandler _handler;

    public AddSalesOrderLineCommandHandlerTests()
    {
        _salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
        _handler = new AddSalesOrderLineCommandHandler(_salesOrderRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_SalesOrder_Not_Found()
    {
        var salesOrderId = SalesOrderId.New();
        _salesOrderRepositoryMock.Setup(r => r.GetByIdAsync(salesOrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((SalesOrder?)null);

        var command = new AddSalesOrderLineCommand(salesOrderId.Value, "Widget", 2, 10m, "USD");
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be("NotFound");
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_SalesOrder_Not_In_Draft_Status()
    {
        var salesOrderId = SalesOrderId.New();
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        order.Approve();

        _salesOrderRepositoryMock.Setup(r => r.GetByIdAsync(salesOrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var command = new AddSalesOrderLineCommand(salesOrderId.Value, "Widget", 2, 10m, "USD");
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be("Validation");
    }

    [Fact]
    public async Task Handle_Should_Add_Line_And_Return_Success_With_LineId()
    {
        var salesOrderId = SalesOrderId.New();
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        _salesOrderRepositoryMock.Setup(r => r.GetByIdAsync(salesOrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);
        _salesOrderRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<SalesOrder>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var command = new AddSalesOrderLineCommand(salesOrderId.Value, "Widget", 2, 10m, "USD");
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
        order.Lines.Should().HaveCount(1);
        order.Lines[0].ProductName.Should().Be("Widget");
        order.TotalAmount.Should().Be(Money.Create(20m, "USD"));
    }

    [Fact]
    public async Task Handle_Should_Call_UpdateAsync_On_SalesOrderRepository()
    {
        var salesOrderId = SalesOrderId.New();
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        _salesOrderRepositoryMock.Setup(r => r.GetByIdAsync(salesOrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);
        _salesOrderRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<SalesOrder>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var command = new AddSalesOrderLineCommand(salesOrderId.Value, "Widget", 2, 10m, "USD");
        await _handler.Handle(command, CancellationToken.None);

        _salesOrderRepositoryMock.Verify(r => r.UpdateAsync(order, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Allow_Multiple_Lines_On_Draft_Order()
    {
        var salesOrderId = SalesOrderId.New();
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        _salesOrderRepositoryMock.Setup(r => r.GetByIdAsync(salesOrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);
        _salesOrderRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<SalesOrder>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var command1 = new AddSalesOrderLineCommand(salesOrderId.Value, "Widget", 2, 10m, "USD");
        var result1 = await _handler.Handle(command1, CancellationToken.None);

        var command2 = new AddSalesOrderLineCommand(salesOrderId.Value, "Gadget", 3, 5m, "USD");
        var result2 = await _handler.Handle(command2, CancellationToken.None);

        result1.IsSuccess.Should().BeTrue();
        result2.IsSuccess.Should().BeTrue();
        order.Lines.Should().HaveCount(2);
        order.TotalAmount.Should().Be(Money.Create(35m, "USD"));
    }
}
