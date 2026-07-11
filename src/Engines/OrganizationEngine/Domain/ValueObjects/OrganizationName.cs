using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.OrganizationEngine.Domain.ValueObjects;

/// <summary>
/// Represents an organization name.
/// </summary>
public sealed class OrganizationName : ValueObject
{
    /// <summary>
    /// Gets the maximum length for organization name.
    /// </summary>
    public const int MaxLength = 200;

    /// <summary>
    /// Gets the organization name value.
    /// </summary>
    public string Value { get; }

    private OrganizationName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new organization name.
    /// </summary>
    /// <param name="value">The name value.</param>
    /// <returns>A new OrganizationName instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the name is null/empty or exceeds max length.</exception>
    public static OrganizationName Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Organization name cannot exceed {MaxLength} characters.");

        return new OrganizationName(value);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not OrganizationName otherName) return false;
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