using Eleraki.OrganizationEngine.Domain.Organizations;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.OrganizationEngine.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core DbContext for the OrganizationEngine.
/// </summary>
public class OrganizationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationDbContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public OrganizationDbContext(DbContextOptions<OrganizationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets the Organizations DbSet.
    /// </summary>
    public DbSet<Organization> Organizations => Set<Organization>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
