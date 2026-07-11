namespace Eleraki.SharedKernel.Abstractions;

/// <summary>
/// Represents a domain event.
/// </summary>
public interface IDomainEvent
{
    /// Gets the event identifier.
    Guid Id { get; }

    /// <summary>
    /// Gets the date and time when the event occurred.
    /// </summary>
    DateTime OccurredOn { get; }
}