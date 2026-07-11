using Eleraki.AuthorizationEngine.Domain.Repositories;
using Eleraki.AuthorizationEngine.Infrastructure.Persistence;
using Eleraki.AuthorizationEngine.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.AuthorizationEngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthorizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<AuthorizationDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<AuthorizationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();

        return services;
    }
}
