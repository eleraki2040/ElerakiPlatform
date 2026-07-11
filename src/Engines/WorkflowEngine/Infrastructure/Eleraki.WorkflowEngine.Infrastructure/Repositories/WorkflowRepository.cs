using Eleraki.WorkflowEngine.Domain;
using Eleraki.WorkflowEngine.Domain.Repositories;
using Eleraki.WorkflowEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.WorkflowEngine.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Workflow aggregate.
/// </summary>
public class WorkflowRepository : IWorkflowRepository
{
    private readonly WorkflowDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkflowRepository"/> class.
    /// </summary>
    /// <param name="context">The Workflow DbContext.</param>
    public WorkflowRepository(WorkflowDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Workflow?> GetByIdAsync(WorkflowId id, CancellationToken cancellationToken = default)
    {
        return await _context.Workflows.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Workflow>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Workflows.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(Workflow workflow, CancellationToken cancellationToken = default)
    {
        await _context.Workflows.AddAsync(workflow, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Workflow workflow, CancellationToken cancellationToken = default)
    {
        _context.Workflows.Update(workflow);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Workflow workflow, CancellationToken cancellationToken = default)
    {
        _context.Workflows.Remove(workflow);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
