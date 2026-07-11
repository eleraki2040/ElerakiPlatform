namespace Eleraki.WorkflowEngine.Domain.Repositories;

/// <summary>
/// Repository interface for Workflow aggregate.
/// </summary>
public interface IWorkflowRepository
{
    /// <summary>
    /// Gets a workflow by its ID.
    /// </summary>
    Task<Workflow?> GetByIdAsync(WorkflowId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all workflows.
    /// </summary>
    Task<IReadOnlyList<Workflow>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new workflow.
    /// </summary>
    Task AddAsync(Workflow workflow, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing workflow.
    /// </summary>
    Task UpdateAsync(Workflow workflow, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a workflow.
    /// </summary>
    Task DeleteAsync(Workflow workflow, CancellationToken cancellationToken = default);
}
