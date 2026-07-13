using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.SharedKernel.Events;

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

/// <summary>
/// Domain event raised when an organization is archived.
/// </summary>
public sealed record OrganizationArchivedDomainEvent(OrganizationId OrganizationId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit type is created.
/// </summary>
public sealed record OrganizationUnitTypeCreatedDomainEvent(OrganizationUnitTypeId OrganizationUnitTypeId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit type is updated.
/// </summary>
public sealed record OrganizationUnitTypeUpdatedDomainEvent(OrganizationUnitTypeId OrganizationUnitTypeId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit type is activated.
/// </summary>
public sealed record OrganizationUnitTypeActivatedDomainEvent(OrganizationUnitTypeId OrganizationUnitTypeId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit type is deactivated.
/// </summary>
public sealed record OrganizationUnitTypeDeactivatedDomainEvent(OrganizationUnitTypeId OrganizationUnitTypeId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit is created.
/// </summary>
public sealed record OrganizationUnitCreatedDomainEvent(OrganizationUnitId OrganizationUnitId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit is updated.
/// </summary>
public sealed record OrganizationUnitUpdatedDomainEvent(OrganizationUnitId OrganizationUnitId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit is moved.
/// </summary>
public sealed record OrganizationUnitMovedDomainEvent(OrganizationUnitId OrganizationUnitId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit is activated.
/// </summary>
public sealed record OrganizationUnitActivatedDomainEvent(OrganizationUnitId OrganizationUnitId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit is deactivated.
/// </summary>
public sealed record OrganizationUnitDeactivatedDomainEvent(OrganizationUnitId OrganizationUnitId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);

/// <summary>
/// Domain event raised when an organization unit is archived.
/// </summary>
public sealed record OrganizationUnitArchivedDomainEvent(OrganizationUnitId OrganizationUnitId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
