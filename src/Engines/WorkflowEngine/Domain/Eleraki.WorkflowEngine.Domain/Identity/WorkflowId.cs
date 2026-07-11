using Eleraki.SharedKernel.Identity;

namespace Eleraki.WorkflowEngine.Domain;

/// <summary>
/// Represents the unique identifier for a Workflow.
/// </summary>
public sealed record WorkflowId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    /// <summary>
    /// Creates a new WorkflowId with a new GUID.
    /// </summary>
    public static WorkflowId New() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new WorkflowId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    public static WorkflowId From(Guid value) => new(value);
}
