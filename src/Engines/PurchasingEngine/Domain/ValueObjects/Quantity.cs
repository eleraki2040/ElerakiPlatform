using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.PurchasingEngine.Domain.ValueObjects;

public sealed class Quantity : ValueObject
{
    public int Value { get; }

    private Quantity(int value)
    {
        Value = value;
    }

    public static Quantity Create(int value)
    {
        if (value < 0)
            throw new ArgumentException("Quantity cannot be negative.", nameof(value));

        return new Quantity(value);
    }

    public static Quantity operator +(Quantity left, Quantity right) => new(left.Value + right.Value);
    public static Quantity operator -(Quantity left, Quantity right) => new(left.Value - right.Value);

    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not Quantity otherQuantity) return false;
        return Value == otherQuantity.Value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
