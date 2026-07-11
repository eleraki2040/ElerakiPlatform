using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.IdentityEngine.Domain;

/// <summary>
/// Represents a user name.
/// </summary>
public sealed class UserName : ValueObject
{
    /// <summary>
    /// Gets the maximum length for user name.
    /// </summary>
    public const int MaxLength = 100;

    /// <summary>
    /// Gets the user name value.
    /// </summary>
    public string Value { get; }

    private UserName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new user name.
    /// </summary>
    /// <param name="value">The name value.</param>
    /// <returns>A new UserName instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the name is invalid.</exception>
    public static UserName Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"User name cannot exceed {MaxLength} characters.");

        return new UserName(value);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not UserName otherName) return false;
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
