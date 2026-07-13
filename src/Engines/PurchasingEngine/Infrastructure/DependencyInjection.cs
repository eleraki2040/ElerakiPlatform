using Eleraki.PurchasingEngine.Infrastructure.Persistence;
using Eleraki.PurchasingEngine.Infrastructure.Repositories;
using Eleraki.PurchasingEngine.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.PurchasingEngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPurchasingEngineInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=eleraki_purchasing.db";

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<PurchasingDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<PurchasingDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();

        return services;
    }
}
