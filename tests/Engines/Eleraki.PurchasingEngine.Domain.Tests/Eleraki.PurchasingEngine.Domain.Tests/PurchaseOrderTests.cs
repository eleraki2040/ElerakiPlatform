using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using FluentAssertions;

namespace Eleraki.PurchasingEngine.Domain.Tests;

public class PurchaseOrderTests
{
    [Fact]
    public void Create_ShouldReturnOrderWithDraftStatus()
    {
        var vendorId = VendorId.New();
        var order = PurchaseOrder.Create(vendorId);

        order.VendorId.Should().Be(vendorId);
        order.Status.Should().Be(PurchaseOrderStatus.Draft);
        order.TotalAmount.Amount.Should().Be(0);
        order.Lines.Should().BeEmpty();
    }

    [Fact]
    public void AddLine_ShouldAddLineAndRecalculateTotal()
    {
        var vendorId = VendorId.New();
        var order = PurchaseOrder.Create(vendorId);
        var lineId = PurchaseOrderLineId.New();
        var unitPrice = Money.Create(10m, "USD");

        order.AddLine(lineId, "Widget", 2, unitPrice);

        order.Lines.Should().HaveCount(1);
        order.TotalAmount.Amount.Should().Be(20m);
    }

    [Fact]
    public void Submit_ShouldChangeStatusToSubmitted()
    {
        var vendorId = VendorId.New();
        var order = PurchaseOrder.Create(vendorId);

        order.Submit();

        order.Status.Should().Be(PurchaseOrderStatus.Submitted);
    }

    [Fact]
    public void Approve_ShouldChangeStatusToApproved()
    {
        var vendorId = VendorId.New();
        var order = PurchaseOrder.Create(vendorId);
        order.Submit();

        order.Approve();

        order.Status.Should().Be(PurchaseOrderStatus.Approved);
    }

    [Fact]
    public void Cancel_ShouldChangeStatusToCancelled()
    {
        var vendorId = VendorId.New();
        var order = PurchaseOrder.Create(vendorId);

        order.Cancel();

        order.Status.Should().Be(PurchaseOrderStatus.Cancelled);
    }
}
