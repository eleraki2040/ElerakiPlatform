using Eleraki.SharedKernel.Events;

namespace Eleraki.AuthorizationEngine.Domain.Events;

public sealed record PermissionCreatedDomainEvent(PermissionId PermissionId) : DomainEvent;
public sealed record PermissionUpdatedDomainEvent(PermissionId PermissionId) : DomainEvent;
public sealed record PermissionDeletedDomainEvent(PermissionId PermissionId) : DomainEvent;
public sealed record RoleCreatedDomainEvent(RoleId RoleId) : DomainEvent;
public sealed record RoleUpdatedDomainEvent(RoleId RoleId) : DomainEvent;
public sealed record RoleDeletedDomainEvent(RoleId RoleId) : DomainEvent;
public sealed record RoleActivatedDomainEvent(RoleId RoleId) : DomainEvent;
public sealed record RoleDeactivatedDomainEvent(RoleId RoleId) : DomainEvent;
