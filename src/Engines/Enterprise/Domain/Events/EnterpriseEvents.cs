using Eleraki.SharedKernel.Events;

namespace Eleraki.Enterprise.Domain.Events;

/// <summary>
/// Domain event raised when an enterprise is created.
/// </summary>
public sealed record EnterpriseCreatedDomainEvent(EnterpriseId EnterpriseId) : DomainEvent;

/// <summary>
/// Domain event raised when an enterprise is updated.
/// </summary>
public sealed record EnterpriseUpdatedDomainEvent(EnterpriseId EnterpriseId) : DomainEvent;

/// <summary>
/// Domain event raised when an enterprise is activated.
/// </summary>
public sealed record EnterpriseActivatedDomainEvent(EnterpriseId EnterpriseId) : DomainEvent;

/// <summary>
/// Domain event raised when an enterprise is deactivated.
/// </summary>
public sealed record EnterpriseDeactivatedDomainEvent(EnterpriseId EnterpriseId) : DomainEvent;

/// <summary>
/// Domain event raised when an enterprise is suspended.
/// </summary>
public sealed record EnterpriseSuspendedDomainEvent(EnterpriseId EnterpriseId) : DomainEvent;

/// <summary>
/// Domain event raised when an enterprise is archived.
/// </summary>
public sealed record EnterpriseArchivedDomainEvent(EnterpriseId EnterpriseId) : DomainEvent;