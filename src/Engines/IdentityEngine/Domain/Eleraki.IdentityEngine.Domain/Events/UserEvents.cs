using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Events;

namespace Eleraki.IdentityEngine.Domain.Events;

/// <summary>
/// Domain event raised when a User is created.
/// </summary>
public sealed record UserCreatedDomainEvent(UserId UserId) : DomainEvent;

/// <summary>
/// Domain event raised when a User is activated.
/// </summary>
public sealed record UserActivatedDomainEvent(UserId UserId) : DomainEvent;

/// <summary>
/// Domain event raised when a User is deactivated.
/// </summary>
public sealed record UserDeactivatedDomainEvent(UserId UserId) : DomainEvent;

/// <summary>
/// Domain event raised when a User role is changed.
/// </summary>
public sealed record UserRoleChangedDomainEvent(UserId UserId, UserRole NewRole) : DomainEvent;
