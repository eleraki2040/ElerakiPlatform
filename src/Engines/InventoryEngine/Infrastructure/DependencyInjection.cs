using Eleraki.InventoryEngine.Infrastructure.Persistence;
using Eleraki.InventoryEngine.Infrastructure.Repositories;
using Eleraki.InventoryEngine.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.InventoryEngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInventoryEngineInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=eleraki_inventory.db";

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();

        return services;
    }
}
