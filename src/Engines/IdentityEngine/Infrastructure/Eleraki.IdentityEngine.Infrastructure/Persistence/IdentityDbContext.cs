using Eleraki.IdentityEngine.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.IdentityEngine.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core DbContext for the Identity Engine.
/// </summary>
public class IdentityDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityDbContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets the Users DbSet.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
