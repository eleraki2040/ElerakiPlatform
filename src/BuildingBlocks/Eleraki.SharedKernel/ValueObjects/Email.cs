using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SharedKernel.ValueObjects;

/// <summary>
/// Represents an email address.
/// </summary>
public sealed class Email : ValueObject
{
    /// <summary>
    /// Gets the maximum length for email.
    /// </summary>
    public const int MaxLength = 254;

    /// <summary>
    /// Gets the email value.
    /// </summary>
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new email.
    /// </summary>
    /// <param name="value">The email value.</param>
    /// <returns>A new Email instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the email is invalid.</exception>
    public static Email Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Email cannot exceed {MaxLength} characters.");

        if (!value.Contains('@') || !value.Contains('.'))
            throw new ArgumentException("Invalid email format.");

        return new Email(value);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not Email otherEmail) return false;
        return Value == otherEmail.Value;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <inheritdoc/>
    public override string ToString() => Value;
}
