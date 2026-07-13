using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SharedKernel.ValueObjects;

/// <summary>
/// Represents a monetary amount.
/// </summary>
public sealed class Money : ValueObject
{
    /// <summary>
    /// Gets the monetary amount.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Gets the currency code.
    /// </summary>
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// Creates a new Money instance.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <param name="currency">The currency code.</param>
    /// <returns>A new Money instance.</returns>
    public static Money Create(decimal amount, string currency)
    {
        Guard.NotNullOrEmpty(currency, nameof(currency));

        if (amount < 0)
            throw new ArgumentException("Money amount cannot be negative.", nameof(amount));

        return new Money(amount, currency);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not Money otherMoney) return false;
        return Amount == otherMoney.Amount && Currency == otherMoney.Currency;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount:N2} {Currency}";

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new ArgumentException("Currency mismatch.");
    }
}
