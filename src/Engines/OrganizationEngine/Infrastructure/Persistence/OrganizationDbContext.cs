using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.Organizations;
using Eleraki.OrganizationEngine.Domain.OrganizationUnits;
using Eleraki.OrganizationEngine.Domain.OrganizationUnitTypes;
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

    /// <summary>
    /// Gets the OrganizationUnits DbSet.
    /// </summary>
    public DbSet<OrganizationUnit> OrganizationUnits => Set<OrganizationUnit>();

    /// <summary>
    /// Gets the OrganizationUnitTypes DbSet.
    /// </summary>
    public DbSet<OrganizationUnitType> OrganizationUnitTypes => Set<OrganizationUnitType>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
