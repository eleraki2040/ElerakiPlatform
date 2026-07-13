using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.InventoryEngine.Domain.ValueObjects;

public sealed class Location : ValueObject
{
    public string Value { get; }

    private Location(string value)
    {
        Value = value;
    }

    public static Location Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim();

        if (value.Length > 200)
            throw new ArgumentException("Location cannot exceed 200 characters.");

        return new Location(value);
    }

    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not Location otherLocation) return false;
        return Value == otherLocation.Value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
