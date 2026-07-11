using Eleraki.SharedKernel.Primitives;

namespace Eleraki.WorkflowEngine.Domain;

/// <summary>
/// Domain errors for Workflow Engine.
/// </summary>
public static class WorkflowErrors
{
    /// <summary>
    /// Workflow not found.
    /// </summary>
    public static readonly Error WorkflowNotFound = Error.NotFound("Workflow not found.");

    /// <summary>
    /// Workflow name already exists.
    /// </summary>
    public static readonly Error WorkflowNameAlreadyExists = Error.Conflict("Workflow name already exists.");

    /// <summary>
    /// Cannot complete inactive workflow.
    /// </summary>
    public static readonly Error CannotCompleteInactiveWorkflow = Error.Validation("Cannot complete an inactive workflow.");
}
