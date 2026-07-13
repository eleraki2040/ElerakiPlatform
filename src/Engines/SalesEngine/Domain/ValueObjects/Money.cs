using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.SalesEngine.Domain.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, string currency)
    {
        Guard.NotNullOrEmpty(currency, nameof(currency));

        if (amount < 0)
            throw new ArgumentException("Money amount cannot be negative.", nameof(amount));

        return new Money(amount, currency);
    }

    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not Money otherMoney) return false;
        return Amount == otherMoney.Amount && Currency == otherMoney.Currency;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount:N2} {Currency}";

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new ArgumentException("Currency mismatch.");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Multiply(int multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("Multiplier cannot be negative.", nameof(multiplier));

        return new Money(Amount * multiplier, Currency);
    }
}
