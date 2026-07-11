using Eleraki.WorkflowEngine.Domain.Repositories;
using Eleraki.WorkflowEngine.Infrastructure.Persistence;
using Eleraki.WorkflowEngine.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.WorkflowEngine.Infrastructure;

/// <summary>
/// Dependency injection registration for Workflow Engine Infrastructure.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Workflow Engine Infrastructure services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddWorkflowInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<WorkflowDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<WorkflowDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<Eleraki.WorkflowEngine.Domain.Repositories.IWorkflowRepository, WorkflowRepository>();

        return services;
    }
}
