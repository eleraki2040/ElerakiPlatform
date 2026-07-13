using Eleraki.HospitalEngine.Infrastructure.Persistence;
using Eleraki.HospitalEngine.Infrastructure.Repositories;
using Eleraki.HospitalEngine.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.HospitalEngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddHospitalEngineInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=eleraki_hospital.db";

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<HospitalDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<HospitalDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<IHospitalRepository, HospitalRepository>();

        return services;
    }
}
