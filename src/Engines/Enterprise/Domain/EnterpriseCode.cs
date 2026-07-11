using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Represents an enterprise code (Unique Business Identifier).
/// </summary>
public sealed class EnterpriseCode : ValueObject
{
    /// <summary>
    /// Gets the maximum length for enterprise code.
    /// </summary>
    public const int MaxLength = 50;

    /// <summary>
    /// Gets the enterprise code value.
    /// </summary>
    public string Value { get; }

    private EnterpriseCode(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new enterprise code.
    /// </summary>
    /// <param name="value">The code value.</param>
    /// <returns>A new EnterpriseCode instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the code is null/empty or exceeds max length.</exception>
    public static EnterpriseCode Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim().ToUpperInvariant();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Enterprise code cannot exceed {MaxLength} characters.");

        return new EnterpriseCode(value);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not EnterpriseCode otherCode) return false;
        return Value == otherCode.Value;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <inheritdoc/>
    public override string ToString() => Value;
}