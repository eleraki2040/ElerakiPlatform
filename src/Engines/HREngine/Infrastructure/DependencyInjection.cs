using Eleraki.HREngine.Infrastructure.Persistence;
using Eleraki.HREngine.Infrastructure.Repositories;
using Eleraki.HREngine.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.HREngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddHREngineInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("HRConnection")
            ?? configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=eleraki_hr.db";

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<HRDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<HRDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<ISalaryRepository, SalaryRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<ILeaveRepository, LeaveRepository>();

        return services;
    }
}
