using Eleraki.WorkflowEngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.WorkflowEngine.Domain;

/// <summary>
/// Represents a workflow in the system.
/// </summary>
public sealed class Workflow : AggregateRoot<WorkflowId>
{
    /// <summary>
    /// Gets the workflow name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the workflow description.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the workflow status.
    /// </summary>
    public WorkflowStatus Status { get; private set; }

    /// <summary>
    /// Gets the creation date and time.
    /// </summary>
    public DateTime CreatedOn { get; private set; }

    /// <summary>
    /// Gets the last modification date and time.
    /// </summary>
    public DateTime ModifiedOn { get; private set; }

    private Workflow(WorkflowId id)
        : base(id)
    {
    }

    /// <summary>
    /// Creates a new workflow.
    /// </summary>
    /// <param name="name">The workflow name.</param>
    /// <param name="description">The workflow description.</param>
    /// <returns>A new Workflow instance.</returns>
    public static Workflow Create(string name, string? description = null)
    {
        var workflow = new Workflow(WorkflowId.New())
        {
            Name = name,
            Description = description,
            Status = WorkflowStatus.Active,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        workflow.RaiseDomainEvent(new WorkflowCreatedDomainEvent(workflow.Id));

        return workflow;
    }

    /// <summary>
    /// Activates the workflow.
    /// </summary>
    public void Activate()
    {
        if (Status == WorkflowStatus.Active)
            return;

        Status = WorkflowStatus.Active;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new WorkflowActivatedDomainEvent(Id));
    }

    /// <summary>
    /// Deactivates the workflow.
    /// </summary>
    public void Deactivate()
    {
        if (Status == WorkflowStatus.Inactive)
            return;

        Status = WorkflowStatus.Inactive;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new WorkflowDeactivatedDomainEvent(Id));
    }

    /// <summary>
    /// Completes the workflow.
    /// </summary>
    public void Complete()
    {
        if (Status == WorkflowStatus.Completed)
            return;

        Status = WorkflowStatus.Completed;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new WorkflowCompletedDomainEvent(Id));
    }

    /// <summary>
    /// Cancels the workflow.
    /// </summary>
    public void Cancel()
    {
        if (Status == WorkflowStatus.Cancelled)
            return;

        Status = WorkflowStatus.Cancelled;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new WorkflowCancelledDomainEvent(Id));
    }

    /// <summary>
    /// Updates the workflow information.
    /// </summary>
    /// <param name="name">The new workflow name.</param>
    /// <param name="description">The new workflow description.</param>
    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new WorkflowUpdatedDomainEvent(Id));
    }

    public void Delete()
    {
        RaiseDomainEvent(new WorkflowCancelledDomainEvent(Id));
    }
}
