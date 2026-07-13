using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SharedKernel.ValueObjects;

/// <summary>
/// Represents a person's full name.
/// </summary>
public sealed class PersonName : ValueObject
{
    /// <summary>
    /// Gets the maximum length for person name.
    /// </summary>
    public const int MaxLength = 200;

    /// <summary>
    /// Gets the person name value.
    /// </summary>
    public string Value { get; }

    private PersonName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new person name.
    /// </summary>
    /// <param name="value">The name value.</param>
    /// <returns>A new PersonName instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the name is invalid.</exception>
    public static PersonName Create(string value)
    {
        Guard.NotNullOrEmpty(value, nameof(value));

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Person name cannot exceed {MaxLength} characters.");

        return new PersonName(value);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not PersonName otherName) return false;
        return Value == otherName.Value;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
