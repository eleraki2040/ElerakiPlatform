using Eleraki.SharedKernel.Abstractions;

namespace Eleraki.SharedKernel.Identity;

/// <summary>
/// Base record for strongly typed identifiers.
/// </summary>
/// <remarks>
/// This provides a foundation for creating type-safe identifiers that wrap primitive values.
/// </remarks>
public abstract record StronglyTypedId<TValue> : IStronglyTypedId
    where TValue : struct
{
    /// <summary>
    /// Gets the underlying value of the identifier.
    /// </summary>
    public TValue Value { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StronglyTypedId{TValue}"/> record.
    /// </summary>
    /// <param name="value">The underlying identifier value.</param>
    protected StronglyTypedId(TValue value)
    {
        Value = value;
    }

    /// <inheritdoc/>
    public override string ToString() => Value.ToString() ?? string.Empty;
}