using Eleraki.SalesEngine.Application.Commands;
using Eleraki.SalesEngine.Domain.Customers;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SharedKernel.Primitives;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Eleraki.SalesEngine.Application.Tests;

public class CreateSalesOrderCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<ISalesOrderRepository> _salesOrderRepositoryMock;
    private readonly CreateSalesOrderCommandHandler _handler;

    public CreateSalesOrderCommandHandlerTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
        _handler = new CreateSalesOrderCommandHandler(_customerRepositoryMock.Object, _salesOrderRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Customer_Not_Found()
    {
        var customerId = CustomerId.New();
        _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        var command = new CreateSalesOrderCommand(customerId.Value, "Eleraki", "SO-001");
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error!.Code.Should().Be("NotFound");
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_OrderNumber_Exists()
    {
        var customerId = CustomerId.New();
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");
        _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);
        _salesOrderRepositoryMock.Setup(r => r.ExistsByOrderNumberAsync("SO-001", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new CreateSalesOrderCommand(customerId.Value, "Eleraki", "SO-001");
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be("Conflict");
    }

    [Fact]
    public async Task Handle_Should_Return_Success_With_SalesOrderId_When_Valid()
    {
        var customerId = CustomerId.New();
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");
        _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);
        _salesOrderRepositoryMock.Setup(r => r.ExistsByOrderNumberAsync("SO-001", It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _salesOrderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<SalesOrder>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var command = new CreateSalesOrderCommand(customerId.Value, "Eleraki", "SO-001");
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_Call_AddAsync_On_SalesOrderRepository()
    {
        var customerId = CustomerId.New();
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");
        _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);
        _salesOrderRepositoryMock.Setup(r => r.ExistsByOrderNumberAsync("SO-001", It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _salesOrderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<SalesOrder>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var command = new CreateSalesOrderCommand(customerId.Value, "Eleraki", "SO-001");
        await _handler.Handle(command, CancellationToken.None);

        _salesOrderRepositoryMock.Verify(r => r.AddAsync(It.Is<SalesOrder>(o => o.OrderNumber == "SO-001"), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Set_CustomerId_And_CustomerName_On_SalesOrder()
    {
        var customerId = CustomerId.New();
        var customer = Customer.Create("Eleraki", "info@eleraki.com", "+254700000000");
        _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);
        _salesOrderRepositoryMock.Setup(r => r.ExistsByOrderNumberAsync("SO-001", It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        SalesOrder? capturedOrder = null;
        _salesOrderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<SalesOrder>(), It.IsAny<CancellationToken>()))
            .Callback<SalesOrder, CancellationToken>((o, _) => capturedOrder = o)
            .Returns(Task.CompletedTask);

        var command = new CreateSalesOrderCommand(customerId.Value, "Eleraki", "SO-001");
        await _handler.Handle(command, CancellationToken.None);

        capturedOrder.Should().NotBeNull();
        capturedOrder!.CustomerId.Should().Be(customerId);
        capturedOrder.CustomerName.Should().Be("Eleraki");
    }
}
