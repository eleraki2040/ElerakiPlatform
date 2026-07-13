using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SalesEngine.Domain.SalesOrderLines;

public sealed class SalesOrderLine : AggregateRoot<SalesOrderLineId>
{
    public SalesOrderId SalesOrderId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public Quantity Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = null!;
    public Money TotalPrice { get; private set; } = null!;

    private SalesOrderLine(SalesOrderLineId id) : base(id)
    {
    }

    public static SalesOrderLine Create(SalesOrderId salesOrderId, string productName, int quantity, Money unitPrice)
    {
        Guard.NotNull(salesOrderId, nameof(salesOrderId));
        Guard.NotNullOrEmpty(productName, nameof(productName));
        Guard.NotNull(unitPrice, nameof(unitPrice));

        var line = new SalesOrderLine(SalesOrderLineId.New())
        {
            SalesOrderId = salesOrderId,
            ProductName = productName,
            Quantity = Quantity.Create(quantity),
            UnitPrice = unitPrice,
            TotalPrice = unitPrice.Multiply(quantity)
        };

        return line;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = Quantity.Create(quantity);
        TotalPrice = UnitPrice.Multiply(quantity);
    }
}
