using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.PurchasingEngine.Domain.Entities;

public sealed class PurchaseOrderLine : Entity<PurchaseOrderLineId>
{
    public PurchaseOrderId PurchaseOrderId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public Quantity Quantity { get; private set; } = null!;
    public Money UnitPrice { get; private set; } = null!;
    public Money LineTotal { get; private set; } = null!;

    private PurchaseOrderLine(PurchaseOrderLineId id) : base(id)
    {
    }

    public static PurchaseOrderLine Create(PurchaseOrderLineId id, PurchaseOrderId purchaseOrderId, string productName, int quantity, Money unitPrice)
    {
        Guard.NotNull(purchaseOrderId, nameof(purchaseOrderId));
        Guard.NotNullOrEmpty(productName, nameof(productName));
        Guard.NotNull(unitPrice, nameof(unitPrice));

        var line = new PurchaseOrderLine(id)
        {
            PurchaseOrderId = purchaseOrderId,
            ProductName = productName,
            Quantity = Quantity.Create(quantity),
            UnitPrice = unitPrice
        };

        line.LineTotal = Money.Create(unitPrice.Amount * line.Quantity.Value, unitPrice.Currency);

        return line;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = Quantity.Create(quantity);
        LineTotal = Money.Create(UnitPrice.Amount * quantity, UnitPrice.Currency);
    }

    public void UpdateUnitPrice(Money unitPrice)
    {
        Guard.NotNull(unitPrice, nameof(unitPrice));
        UnitPrice = unitPrice;
        LineTotal = Money.Create(unitPrice.Amount * Quantity.Value, unitPrice.Currency);
    }
}
