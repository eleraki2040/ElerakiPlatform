using Eleraki.SharedKernel.Events;

namespace Eleraki.WorkflowEngine.Domain.Events;

/// <summary>
/// Domain event raised when a Workflow is created.
/// </summary>
public sealed record WorkflowCreatedDomainEvent(WorkflowId WorkflowId) : DomainEvent;

/// <summary>
/// Domain event raised when a Workflow is updated.
/// </summary>
public sealed record WorkflowUpdatedDomainEvent(WorkflowId WorkflowId) : DomainEvent;

/// <summary>
/// Domain event raised when a Workflow is activated.
/// </summary>
public sealed record WorkflowActivatedDomainEvent(WorkflowId WorkflowId) : DomainEvent;

/// <summary>
/// Domain event raised when a Workflow is deactivated.
/// </summary>
public sealed record WorkflowDeactivatedDomainEvent(WorkflowId WorkflowId) : DomainEvent;

/// <summary>
/// Domain event raised when a Workflow is completed.
/// </summary>
public sealed record WorkflowCompletedDomainEvent(WorkflowId WorkflowId) : DomainEvent;

/// <summary>
/// Domain event raised when a Workflow is cancelled.
/// </summary>
public sealed record WorkflowCancelledDomainEvent(WorkflowId WorkflowId) : DomainEvent;
