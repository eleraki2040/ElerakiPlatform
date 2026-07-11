using Eleraki.SharedKernel.Abstractions;

namespace Eleraki.SharedKernel.Events;

/// <summary>
/// Base implementation for domain events.
/// </summary>
public abstract record DomainEvent(Guid Id, DateTime OccurredOn) : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEvent"/> class.
    /// </summary>
    protected DomainEvent() : this(Guid.NewGuid(), DateTime.UtcNow)
    {
    }
}