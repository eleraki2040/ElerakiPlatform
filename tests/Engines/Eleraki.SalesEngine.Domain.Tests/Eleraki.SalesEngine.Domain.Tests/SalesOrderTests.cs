using Eleraki.SalesEngine.Domain.Customers;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SalesEngine.Domain.ValueObjects;
using FluentAssertions;

namespace Eleraki.SalesEngine.Domain.Tests;

public class SalesOrderTests
{
    [Fact]
    public void Create_Should_Return_SalesOrder_With_Draft_Status()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        order.Should().NotBeNull();
        order.OrderNumber.Should().Be("SO-001");
        order.CustomerId.Should().Be(customerId);
        order.CustomerName.Should().Be("Eleraki");
        order.Status.Should().Be(SalesOrderStatus.Draft);
        order.TotalAmount.Should().Be(Money.Create(0, "USD"));
        order.Lines.Should().BeEmpty();
        order.Id.Should().NotBe(default(SalesOrderId));
    }

    [Fact]
    public void Create_Should_Raise_SalesOrderCreatedDomainEvent()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        order.DomainEvents.Should().Contain(e => e.GetType().Name == "SalesOrderCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_OrderNumber_Is_Null()
    {
        var customerId = CustomerId.New();

        Action act = () => SalesOrder.Create(null!, customerId, "Eleraki");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_CustomerId_Is_Default()
    {
        Action act = () => SalesOrder.Create("SO-001", default, "Eleraki");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_CustomerName_Is_Null()
    {
        var customerId = CustomerId.New();

        Action act = () => SalesOrder.Create("SO-001", customerId, null!);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void AddLine_Should_Add_Line_And_Update_TotalAmount()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        var unitPrice = Money.Create(10m, "USD");
        var line = SalesOrderLine.Create(order.Id, "Widget", 2, unitPrice);

        order.AddLine(line);

        order.Lines.Should().HaveCount(1);
        order.Lines[0].Should().Be(line);
        order.TotalAmount.Should().Be(Money.Create(20m, "USD"));
    }

    [Fact]
    public void AddLine_Should_Raise_SalesOrderLineAddedDomainEvent()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        var unitPrice = Money.Create(10m, "USD");
        var line = SalesOrderLine.Create(order.Id, "Widget", 2, unitPrice);
        order.ClearDomainEvents();

        order.AddLine(line);

        order.DomainEvents.Should().Contain(e => e.GetType().Name == "SalesOrderLineAddedDomainEvent");
    }

    [Fact]
    public void AddLine_Should_Recalculate_Total_When_Multiple_Lines()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        var unitPrice1 = Money.Create(10m, "USD");
        var unitPrice2 = Money.Create(5m, "USD");
        var line1 = SalesOrderLine.Create(order.Id, "Widget", 2, unitPrice1);
        var line2 = SalesOrderLine.Create(order.Id, "Gadget", 3, unitPrice2);

        order.AddLine(line1);
        order.AddLine(line2);

        order.Lines.Should().HaveCount(2);
        order.TotalAmount.Should().Be(Money.Create(35m, "USD"));
    }

    [Fact]
    public void Approve_Should_Change_Status_To_Approved()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        order.Approve();

        order.Status.Should().Be(SalesOrderStatus.Approved);
    }

    [Fact]
    public void Approve_Should_Raise_SalesOrderApprovedDomainEvent()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        order.ClearDomainEvents();

        order.Approve();

        order.DomainEvents.Should().Contain(e => e.GetType().Name == "SalesOrderApprovedDomainEvent");
    }

    [Fact]
    public void Approve_Should_Be_Idempotent()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        order.Approve();
        order.ClearDomainEvents();
        order.Approve();

        order.Status.Should().Be(SalesOrderStatus.Approved);
        order.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void Fulfill_Should_Change_Status_To_Fulfilled()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        order.Approve();

        order.Fulfill();

        order.Status.Should().Be(SalesOrderStatus.Fulfilled);
    }

    [Fact]
    public void Fulfill_Should_Raise_SalesOrderFulfilledDomainEvent()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        order.Approve();
        order.ClearDomainEvents();

        order.Fulfill();

        order.DomainEvents.Should().Contain(e => e.GetType().Name == "SalesOrderFulfilledDomainEvent");
    }

    [Fact]
    public void Fulfill_Should_Be_Idempotent()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        order.Approve();

        order.Fulfill();
        order.ClearDomainEvents();
        order.Fulfill();

        order.Status.Should().Be(SalesOrderStatus.Fulfilled);
        order.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void Cancel_Should_Change_Status_To_Cancelled()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        order.Cancel();

        order.Status.Should().Be(SalesOrderStatus.Cancelled);
    }

    [Fact]
    public void Cancel_Should_Raise_SalesOrderCancelledDomainEvent()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");
        order.ClearDomainEvents();

        order.Cancel();

        order.DomainEvents.Should().Contain(e => e.GetType().Name == "SalesOrderCancelledDomainEvent");
    }

    [Fact]
    public void Cancel_Should_Be_Idempotent()
    {
        var customerId = CustomerId.New();
        var order = SalesOrder.Create("SO-001", customerId, "Eleraki");

        order.Cancel();
        order.ClearDomainEvents();
        order.Cancel();

        order.Status.Should().Be(SalesOrderStatus.Cancelled);
        order.DomainEvents.Should().BeEmpty();
    }
}
