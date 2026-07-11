using Eleraki.Enterprise.Domain.Repositories;
using Eleraki.Enterprise.Infrastructure.Persistence;
using Eleraki.Enterprise.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.Enterprise.Infrastructure;

/// <summary>
/// Dependency injection registration for Enterprise Engine Infrastructure.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Enterprise Engine Infrastructure services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddEnterpriseInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<EnterpriseDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<EnterpriseDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<IPartyRepository, PartyRepository>();
        services.AddScoped<IEnterpriseRepository, Repositories.EnterpriseRepository>();

        return services;
    }
}
