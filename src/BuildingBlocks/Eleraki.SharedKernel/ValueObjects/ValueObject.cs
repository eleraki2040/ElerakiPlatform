using Eleraki.SharedKernel.Abstractions;

namespace Eleraki.SharedKernel.ValueObjects;

/// <summary>
/// Base implementation for value objects.
/// </summary>
public abstract class ValueObject : IValueObject
{
    /// <inheritdoc/>
    public bool Equals(IValueObject? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualsCore(other);
    }

    /// <summary>
    /// Determines whether the specified value object is equal to the current value object.
    /// </summary>
    /// <param name="other">The value object to compare with.</param>
    /// <returns>True if the value objects are equal; otherwise, false.</returns>
    protected abstract bool EqualsCore(IValueObject other);

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is IValueObject valueObject) return EqualsCore(valueObject);
        return false;
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(GetEqualityComponents());

    /// <summary>
    /// Gets the components used for equality comparison.
    /// </summary>
    /// <returns>A sequence of objects.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();
}