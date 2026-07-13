using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Events;
using Eleraki.DeliveryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.DeliveryEngine.Domain.Deliveries;

public sealed class DeliveryLine
{
    public Guid Id { get; private set; }
    public Guid DeliveryId { get; private set; }
    public string ProductDescription { get; private set; } = string.Empty;
    public Quantity Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money LineTotal { get; private set; }
    public DateTime CreatedOn { get; private set; }

    private DeliveryLine()
    {
    }

    public static DeliveryLine Create(Guid deliveryId, string productDescription, Quantity quantity, Money unitPrice)
    {
        var lineTotal = Money.Create(quantity.Value * unitPrice.Amount, unitPrice.Currency);

        var line = new DeliveryLine
        {
            Id = Guid.NewGuid(),
            DeliveryId = deliveryId,
            ProductDescription = productDescription,
            Quantity = quantity,
            UnitPrice = unitPrice,
            LineTotal = lineTotal,
            CreatedOn = Clock.UtcNow
        };

        return line;
    }
}
