using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SharedKernel.ValueObjects;

/// <summary>
/// Represents a phone number.
/// </summary>
public sealed class PhoneNumber : ValueObject
{
    /// <summary>
    /// Gets the maximum length for phone number.
    /// </summary>
    public const int MaxLength = 20;

    /// <summary>
    /// Gets the phone number value.
    /// </summary>
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new phone number.
    /// </summary>
    /// <param name="value">The phone number value.</param>
    /// <returns>A new PhoneNumber instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the phone number is invalid.</exception>
    public static PhoneNumber Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Phone number cannot exceed {MaxLength} characters.");

        return new PhoneNumber(value);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not PhoneNumber otherPhone) return false;
        return Value == otherPhone.Value;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <inheritdoc/>
    public override string ToString() => Value;
}
