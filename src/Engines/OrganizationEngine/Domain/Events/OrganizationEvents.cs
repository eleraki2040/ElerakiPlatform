using Eleraki.SharedKernel.Events;
using Eleraki.OrganizationEngine.Domain.Identity;

namespace Eleraki.OrganizationEngine.Domain.Events;

/// <summary>
/// Domain event raised when an organization is created.
/// </summary>
public sealed record OrganizationCreatedDomainEvent(OrganizationId OrganizationId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization is updated.
/// </summary>
public sealed record OrganizationUpdatedDomainEvent(OrganizationId OrganizationId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization is activated.
/// </summary>
public sealed record OrganizationActivatedDomainEvent(OrganizationId OrganizationId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization is deactivated.
/// </summary>
public sealed record OrganizationDeactivatedDomainEvent(OrganizationId OrganizationId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);