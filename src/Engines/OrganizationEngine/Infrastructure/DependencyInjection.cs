using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.OrganizationEngine.Infrastructure.Persistence;
using Eleraki.OrganizationEngine.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.OrganizationEngine.Infrastructure;

/// <summary>
/// Provides extension methods for registering OrganizationEngine infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the OrganizationEngine infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddOrganizationEngineInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=eleraki_organization.db";

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<OrganizationDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<OrganizationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IOrganizationUnitRepository, OrganizationUnitRepository>();
        services.AddScoped<IOrganizationUnitTypeRepository, OrganizationUnitTypeRepository>();

        return services;
    }
}
