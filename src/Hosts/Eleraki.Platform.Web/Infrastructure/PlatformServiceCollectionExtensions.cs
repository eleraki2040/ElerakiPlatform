using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.Platform.Web.Infrastructure;

/// <summary>
/// Platform infrastructure service registration.
/// </summary>
public static class PlatformServiceCollectionExtensions
{
    /// <summary>
    /// Adds platform infrastructure services.
    /// </summary>
    public static IServiceCollection AddPlatformInfrastructure(this IServiceCollection services)
    {
        // Add platform-wide services here
        // Example: logging, caching, health checks, etc.

        return services;
    }
}