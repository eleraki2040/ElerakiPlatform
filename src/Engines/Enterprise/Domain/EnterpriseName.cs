using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Represents an enterprise name.
/// </summary>
public sealed class EnterpriseName : ValueObject
{
    /// <summary>
    /// Gets the maximum length for enterprise name.
    /// </summary>
    public const int MaxLength = 200;

    /// <summary>
    /// Gets the enterprise name value.
    /// </summary>
    public string Value { get; }

    private EnterpriseName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new enterprise name.
    /// </summary>
    /// <param name="value">The name value.</param>
    /// <returns>A new EnterpriseName instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the name is null/empty or exceeds max length.</exception>
    public static EnterpriseName Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Enterprise name cannot exceed {MaxLength} characters.");

        return new EnterpriseName(value);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not EnterpriseName otherName) return false;
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