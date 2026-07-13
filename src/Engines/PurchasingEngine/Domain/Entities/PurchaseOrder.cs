using Eleraki.PurchasingEngine.Domain.Events;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.PurchasingEngine.Domain.Entities;

public sealed class PurchaseOrder : AggregateRoot<PurchaseOrderId>
{
    public VendorId VendorId { get; private set; }
    public PurchaseOrderStatus Status { get; private set; }
    public DateTime OrderDate { get; private set; }
    public DateTime? ExpectedDeliveryDate { get; private set; }
    public Money TotalAmount { get; private set; } = null!;
    public string? Notes { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }
    public List<PurchaseOrderLine> Lines { get; private set; } = new();

    private PurchaseOrder(PurchaseOrderId id) : base(id)
    {
    }

    public static PurchaseOrder Create(VendorId vendorId, DateTime? expectedDeliveryDate = null, string? notes = null)
    {
        Guard.NotNull(vendorId, nameof(vendorId));

        var order = new PurchaseOrder(PurchaseOrderId.New())
        {
            VendorId = vendorId,
            Status = PurchaseOrderStatus.Draft,
            OrderDate = Clock.UtcNow,
            ExpectedDeliveryDate = expectedDeliveryDate,
            Notes = notes,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow,
            TotalAmount = Money.Create(0, "USD")
        };

        order.RaiseDomainEvent(new PurchaseOrderCreatedDomainEvent(order.Id, Guid.NewGuid(), Clock.UtcNow));

        return order;
    }

    public void AddLine(PurchaseOrderLineId lineId, string productName, int quantity, Money unitPrice)
    {
        Guard.NotNull(unitPrice, nameof(unitPrice));

        var line = PurchaseOrderLine.Create(lineId, Id, productName, quantity, unitPrice);
        Lines.Add(line);

        RecalculateTotal();
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PurchaseOrderLineAddedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Submit()
    {
        if (Status != PurchaseOrderStatus.Draft)
            return;

        Status = PurchaseOrderStatus.Submitted;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PurchaseOrderSubmittedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Approve()
    {
        if (Status != PurchaseOrderStatus.Submitted)
            return;

        Status = PurchaseOrderStatus.Approved;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PurchaseOrderApprovedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Receive()
    {
        if (Status != PurchaseOrderStatus.Approved)
            return;

        Status = PurchaseOrderStatus.Received;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PurchaseOrderReceivedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Cancel()
    {
        if (Status is PurchaseOrderStatus.Cancelled or PurchaseOrderStatus.Received)
            return;

        Status = PurchaseOrderStatus.Cancelled;
        ModifiedOn = Clock.UtcNow;
    }

    private void RecalculateTotal()
    {
        TotalAmount = Lines.Aggregate(Money.Create(0, "USD"), (acc, line) => acc.Add(line.LineTotal));
    }
}

public enum PurchaseOrderStatus
{
    Draft = 1,
    Submitted = 2,
    Approved = 3,
    Received = 4,
    Cancelled = 5
}
