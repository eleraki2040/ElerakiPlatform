using Eleraki.SalesEngine.Domain.Events;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SalesEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SalesEngine.Domain.SalesOrders;

public sealed class SalesOrder : AggregateRoot<SalesOrderId>
{
    public string OrderNumber { get; private set; } = null!;
    public CustomerId CustomerId { get; private set; }
    public string CustomerName { get; private set; } = null!;
    public DateTime OrderDate { get; private set; }
    public SalesOrderStatus Status { get; private set; }
    public Money TotalAmount { get; private set; } = null!;
    public List<SalesOrderLine> Lines { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private SalesOrder(SalesOrderId id) : base(id)
    {
    }

    public static SalesOrder Create(string orderNumber, CustomerId customerId, string customerName)
    {
        Guard.NotNullOrEmpty(orderNumber, nameof(orderNumber));
        Guard.NotNull(customerId, nameof(customerId));
        Guard.NotNullOrEmpty(customerName, nameof(customerName));

        var salesOrder = new SalesOrder(SalesOrderId.New())
        {
            OrderNumber = orderNumber,
            CustomerId = customerId,
            CustomerName = customerName,
            OrderDate = Clock.UtcNow,
            Status = SalesOrderStatus.Draft,
            TotalAmount = Money.Create(0, "USD"),
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        salesOrder.RaiseDomainEvent(new SalesOrderCreatedDomainEvent(salesOrder.Id, Guid.NewGuid(), Clock.UtcNow));

        return salesOrder;
    }

    public void AddLine(SalesOrderLine line)
    {
        Lines.Add(line);
        RecalculateTotal();
        UpdatedAt = Clock.UtcNow;
        RaiseDomainEvent(new SalesOrderLineAddedDomainEvent(Id, line.Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Approve()
    {
        if (Status == SalesOrderStatus.Approved)
            return;

        Status = SalesOrderStatus.Approved;
        UpdatedAt = Clock.UtcNow;
        RaiseDomainEvent(new SalesOrderApprovedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Fulfill()
    {
        if (Status == SalesOrderStatus.Fulfilled)
            return;

        Status = SalesOrderStatus.Fulfilled;
        UpdatedAt = Clock.UtcNow;
        RaiseDomainEvent(new SalesOrderFulfilledDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Cancel()
    {
        if (Status == SalesOrderStatus.Cancelled)
            return;

        Status = SalesOrderStatus.Cancelled;
        UpdatedAt = Clock.UtcNow;
        RaiseDomainEvent(new SalesOrderCancelledDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    private void RecalculateTotal()
    {
        var total = Lines.Aggregate(Money.Create(0, "USD"), (acc, line) => acc.Add(line.TotalPrice));
        TotalAmount = total;
    }
}

public enum SalesOrderStatus
{
    Draft = 1,
    Approved = 2,
    Fulfilled = 3,
    Cancelled = 4
}
