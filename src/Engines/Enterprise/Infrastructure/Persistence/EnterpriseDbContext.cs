using Eleraki.Enterprise.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.Enterprise.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core DbContext for the Enterprise Engine.
/// </summary>
public class EnterpriseDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnterpriseDbContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public EnterpriseDbContext(DbContextOptions<EnterpriseDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets the Enterprises DbSet.
    /// </summary>
    public DbSet<Eleraki.Enterprise.Domain.Enterprise> Enterprises => Set<Eleraki.Enterprise.Domain.Enterprise>();

    /// <summary>
    /// Gets the Parties DbSet.
    /// </summary>
    public DbSet<Party> Parties => Set<Party>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnterpriseDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
