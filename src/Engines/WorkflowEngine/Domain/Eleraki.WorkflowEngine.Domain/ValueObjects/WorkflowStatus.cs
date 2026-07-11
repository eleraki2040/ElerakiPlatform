namespace Eleraki.WorkflowEngine.Domain;

/// <summary>
/// Represents the status of a workflow.
/// </summary>
public enum WorkflowStatus
{
    /// <summary>
    /// Workflow is active.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Workflow is inactive.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Workflow is completed.
    /// </summary>
    Completed = 3,

    /// <summary>
    /// Workflow is cancelled.
    /// </summary>
    Cancelled = 4
}
