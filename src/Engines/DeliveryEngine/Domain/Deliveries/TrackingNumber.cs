namespace Eleraki.DeliveryEngine.Domain.Deliveries;

public sealed record TrackingNumber
{
    public string Value { get; }

    private TrackingNumber(string value)
    {
        Value = value;
    }

    public static TrackingNumber New() => new($"TRK-{Guid.NewGuid():N}".ToUpperInvariant());
    public static TrackingNumber From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Tracking number cannot be empty.", nameof(value));
        return new TrackingNumber(value.Trim());
    }
}
