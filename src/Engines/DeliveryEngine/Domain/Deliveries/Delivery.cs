using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.Events;
using Eleraki.DeliveryEngine.Domain.ValueObjects;
using Eleraki.DeliveryEngine.Domain.Vehicles;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.DeliveryEngine.Domain.Deliveries;

public sealed class Delivery
{
    public Guid Id { get; private set; }
    public TrackingNumber TrackingNumber { get; private set; } = null!;
    public string RecipientName { get; private set; } = string.Empty;
    public string DeliveryAddress { get; private set; } = string.Empty;
    public DriverId? DriverId { get; private set; }
    public VehicleId? VehicleId { get; private set; }
    public DeliveryStatus Status { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public string? Notes { get; private set; }
    public Money TotalAmount { get; private set; } = null!;
    public string TotalAmountCurrency { get; private set; } = "USD";
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private readonly List<DeliveryLine> _lines = new();
    public IReadOnlyCollection<DeliveryLine> Lines => _lines.AsReadOnly();

    private Delivery()
    {
    }

    public static Delivery Create(TrackingNumber trackingNumber, string recipientName, string deliveryAddress, DateTime scheduledDate, string? notes = null)
    {
        Guard.NotNull(trackingNumber, nameof(trackingNumber));
        Guard.NotNullOrEmpty(recipientName, nameof(recipientName));
        Guard.NotNullOrEmpty(deliveryAddress, nameof(deliveryAddress));

        var delivery = new Delivery
        {
            Id = Guid.NewGuid(),
            TrackingNumber = trackingNumber,
            RecipientName = recipientName,
            DeliveryAddress = deliveryAddress,
            Status = DeliveryStatus.Pending,
            ScheduledDate = scheduledDate,
            Notes = notes,
            TotalAmount = Money.Create(0, "USD"),
            TotalAmountCurrency = "USD",
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        return delivery;
    }

    public void AssignDriver(DriverId driverId)
    {
        if (Status != DeliveryStatus.Pending && Status != DeliveryStatus.Assigned)
            throw new InvalidOperationException("Cannot assign driver in current delivery status.");

        DriverId = driverId;
        Status = DeliveryStatus.Assigned;
        ModifiedOn = Clock.UtcNow;
    }

    public void Start()
    {
        if (Status != DeliveryStatus.Assigned)
            throw new InvalidOperationException("Cannot start delivery in current status.");

        Status = DeliveryStatus.InTransit;
        ModifiedOn = Clock.UtcNow;
    }

    public void Complete()
    {
        if (Status != DeliveryStatus.InTransit)
            throw new InvalidOperationException("Cannot complete delivery in current status.");

        Status = DeliveryStatus.Completed;
        DeliveredAt = Clock.UtcNow;
        ModifiedOn = Clock.UtcNow;
    }

    public void Cancel(string? reason = null)
    {
        if (Status is DeliveryStatus.Completed or DeliveryStatus.Cancelled)
            throw new InvalidOperationException("Cannot cancel delivery in current status.");

        Status = DeliveryStatus.Cancelled;
        Notes = reason ?? Notes;
        ModifiedOn = Clock.UtcNow;
    }

    public void AddLine(string productDescription, Quantity quantity, Money unitPrice)
    {
        var line = DeliveryLine.Create(Id, productDescription, quantity, unitPrice);
        _lines.Add(line);

        if (TotalAmount.Amount == 0)
        {
            TotalAmount = Money.Create(line.LineTotal.Amount, line.LineTotal.Currency);
            TotalAmountCurrency = line.LineTotal.Currency;
        }
        else if (TotalAmountCurrency == line.LineTotal.Currency)
        {
            TotalAmount = Money.Create(TotalAmount.Amount + line.LineTotal.Amount, TotalAmountCurrency);
        }

        ModifiedOn = Clock.UtcNow;
    }
}

public enum DeliveryStatus
{
    Pending = 1,
    Assigned = 2,
    InTransit = 3,
    Completed = 4,
    Cancelled = 5
}
