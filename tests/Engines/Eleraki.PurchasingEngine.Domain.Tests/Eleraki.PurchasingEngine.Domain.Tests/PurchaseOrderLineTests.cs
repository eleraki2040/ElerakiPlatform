using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using FluentAssertions;

namespace Eleraki.PurchasingEngine.Domain.Tests;

public class PurchaseOrderLineTests
{
    [Fact]
    public void Create_ShouldSetPropertiesCorrectly()
    {
        var lineId = PurchaseOrderLineId.New();
        var purchaseOrderId = PurchaseOrderId.New();
        var unitPrice = Money.Create(5m, "USD");

        var line = PurchaseOrderLine.Create(lineId, purchaseOrderId, "Widget", 3, unitPrice);

        line.ProductName.Should().Be("Widget");
        line.Quantity.Value.Should().Be(3);
        line.UnitPrice.Amount.Should().Be(5m);
        line.LineTotal.Amount.Should().Be(15m);
    }
}
