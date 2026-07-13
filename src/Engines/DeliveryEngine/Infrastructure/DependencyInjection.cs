using Eleraki.DeliveryEngine.Infrastructure.Persistence;
using Eleraki.DeliveryEngine.Infrastructure.Repositories;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.DeliveryEngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDeliveryEngineInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=eleraki_delivery.db";

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<DeliveryDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<DeliveryDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();

        return services;
    }
}
