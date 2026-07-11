using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Represents a party name.
/// </summary>
public sealed class PartyName : ValueObject
{
    /// <summary>
    /// Gets the maximum length for party name.
    /// </summary>
    public const int MaxLength = 200;

    /// <summary>
    /// Gets the party name value.
    /// </summary>
    public string Value { get; }

    private PartyName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new party name.
    /// </summary>
    /// <param name="value">The name value.</param>
    /// <returns>A new PartyName instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the name is null/empty or exceeds max length.</exception>
    public static PartyName Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Party name cannot exceed {MaxLength} characters.");

        return new PartyName(value);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not PartyName otherName) return false;
        return Value == otherName.Value;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <inheritdoc/>
    public override string ToString() => Value;
}
