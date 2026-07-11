using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Events;

namespace Eleraki.Enterprise.Domain.Events;

/// <summary>
/// Domain event raised when a Party is created.
/// </summary>
public sealed record PartyCreatedDomainEvent(PartyId PartyId) : DomainEvent;

/// <summary>
/// Domain event raised when a Party is updated.
/// </summary>
public sealed record PartyUpdatedDomainEvent(PartyId PartyId) : DomainEvent;

/// <summary>
/// Domain event raised when a Party is activated.
/// </summary>
public sealed record PartyActivatedDomainEvent(PartyId PartyId) : DomainEvent;

/// <summary>
/// Domain event raised when a Party is deactivated.
/// </summary>
public sealed record PartyDeactivatedDomainEvent(PartyId PartyId) : DomainEvent;

/// <summary>
/// Domain event raised when a Party is archived.
/// </summary>
public sealed record PartyArchivedDomainEvent(PartyId PartyId) : DomainEvent;
