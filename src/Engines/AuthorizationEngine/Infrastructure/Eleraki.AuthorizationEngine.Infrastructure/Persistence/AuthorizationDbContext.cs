using Eleraki.AuthorizationEngine.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.AuthorizationEngine.Infrastructure.Persistence;

public class AuthorizationDbContext : DbContext
{
    public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options) { }

    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorizationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
