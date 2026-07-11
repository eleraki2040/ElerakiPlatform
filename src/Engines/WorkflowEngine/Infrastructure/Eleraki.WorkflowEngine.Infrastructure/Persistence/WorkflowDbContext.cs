using Eleraki.WorkflowEngine.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.WorkflowEngine.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core DbContext for the Workflow Engine.
/// </summary>
public class WorkflowDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkflowDbContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets the Workflows DbSet.
    /// </summary>
    public DbSet<Workflow> Workflows => Set<Workflow>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkflowDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
