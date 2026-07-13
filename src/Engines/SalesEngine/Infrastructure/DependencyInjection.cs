using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Infrastructure.Persistence;
using Eleraki.SalesEngine.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.SalesEngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSalesEngineInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=eleraki_sales.db";

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<SalesDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<SalesDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
