using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.OrganizationEngine.Domain.ValueObjects;

/// <summary>
/// Represents an organization code.
/// </summary>
public sealed class OrganizationCode : ValueObject
{
    /// <summary>
    /// Gets the maximum length for organization code.
    /// </summary>
    public const int MaxLength = Constants.MaxCodeLength;

    /// <summary>
    /// Gets the organization code value.
    /// </summary>
    public string Value { get; }

    private OrganizationCode(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new organization code.
    /// </summary>
    /// <param name="value">The code value.</param>
    /// <returns>A new OrganizationCode instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the code is null/empty or exceeds max length.</exception>
    public static OrganizationCode Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim().ToUpperInvariant();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Organization code cannot exceed {MaxLength} characters.");

        return new OrganizationCode(value);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not OrganizationCode otherCode) return false;
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