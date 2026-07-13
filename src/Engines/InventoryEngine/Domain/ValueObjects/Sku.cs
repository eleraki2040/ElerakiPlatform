using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.InventoryEngine.Domain.ValueObjects;

public sealed class Sku : ValueObject
{
    public const int MaxLength = 50;

    public string Value { get; }

    private Sku(string value)
    {
        Value = value;
    }

    public static Sku Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim().ToUpperInvariant();

        if (value.Length > MaxLength)
            throw new ArgumentException($"SKU cannot exceed {MaxLength} characters.");

        return new Sku(value);
    }

    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not Sku otherSku) return false;
        return Value == otherSku.Value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
