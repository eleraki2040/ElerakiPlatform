using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.DeliveryEngine.Domain.Deliveries;

public sealed class Quantity : ValueObject
{
    public decimal Value { get; private set; }

    private Quantity(decimal value)
    {
        Value = value;
    }

    public static Quantity Create(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(value));
        return new Quantity(value);
    }

    public static Quantity From(decimal value) => Create(value);

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
