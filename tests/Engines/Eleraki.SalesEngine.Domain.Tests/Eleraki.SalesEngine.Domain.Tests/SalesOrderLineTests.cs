using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SalesEngine.Domain.ValueObjects;
using FluentAssertions;

namespace Eleraki.SalesEngine.Domain.Tests;

public class SalesOrderLineTests
{
    [Fact]
    public void Create_Should_Return_Line_With_Correct_Properties()
    {
        var salesOrderId = SalesOrderId.New();
        var unitPrice = Money.Create(10m, "USD");

        var line = SalesOrderLine.Create(salesOrderId, "Widget", 2, unitPrice);

        line.Should().NotBeNull();
        line.SalesOrderId.Should().Be(salesOrderId);
        line.ProductName.Should().Be("Widget");
        line.Quantity.Value.Should().Be(2);
        line.UnitPrice.Should().Be(unitPrice);
        line.TotalPrice.Should().Be(Money.Create(20m, "USD"));
    }

    [Fact]
    public void Create_Should_Calculate_TotalPrice_From_Quantity_And_UnitPrice()
    {
        var salesOrderId = SalesOrderId.New();
        var unitPrice = Money.Create(5m, "USD");

        var line = SalesOrderLine.Create(salesOrderId, "Gadget", 4, unitPrice);

        line.TotalPrice.Amount.Should().Be(20m);
        line.TotalPrice.Currency.Should().Be("USD");
    }

    [Fact]
    public void Create_Should_Raise_No_DomainEvents()
    {
        var salesOrderId = SalesOrderId.New();
        var unitPrice = Money.Create(10m, "USD");

        var line = SalesOrderLine.Create(salesOrderId, "Widget", 1, unitPrice);

        line.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void UpdateQuantity_Should_Recalculate_TotalPrice()
    {
        var salesOrderId = SalesOrderId.New();
        var unitPrice = Money.Create(10m, "USD");
        var line = SalesOrderLine.Create(salesOrderId, "Widget", 2, unitPrice);

        line.UpdateQuantity(5);

        line.Quantity.Value.Should().Be(5);
        line.TotalPrice.Should().Be(Money.Create(50m, "USD"));
    }

    [Fact]
    public void Create_Should_Throw_When_SalesOrderId_Is_Default()
    {
        var unitPrice = Money.Create(10m, "USD");

        Action act = () => SalesOrderLine.Create(default, "Widget", 2, unitPrice);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_ProductName_Is_Empty()
    {
        var salesOrderId = SalesOrderId.New();
        var unitPrice = Money.Create(10m, "USD");

        Action act = () => SalesOrderLine.Create(salesOrderId, "", 2, unitPrice);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_UnitPrice_Is_Null()
    {
        var salesOrderId = SalesOrderId.New();

        Action act = () => SalesOrderLine.Create(salesOrderId, "Widget", 2, null!);

        act.Should().Throw<ArgumentException>();
    }
}
