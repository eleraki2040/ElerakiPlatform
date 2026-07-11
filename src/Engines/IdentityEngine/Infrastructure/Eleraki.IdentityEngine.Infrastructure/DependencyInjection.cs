using Eleraki.IdentityEngine.Infrastructure.Persistence;
using Eleraki.IdentityEngine.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.IdentityEngine.Infrastructure;

/// <summary>
/// Dependency injection registration for Identity Engine Infrastructure.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Identity Engine Infrastructure services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<Eleraki.IdentityEngine.Domain.Repositories.IUserRepository, UserRepository>();

        return services;
    }
}
