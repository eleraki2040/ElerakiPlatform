using Eleraki.FinanceEngine.Domain;
using Eleraki.FinanceEngine.Infrastructure.Persistence;
using Eleraki.FinanceEngine.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.FinanceEngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddFinanceEngineInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FinanceConnection")
            ?? "Data Source=eleraki_finance.db";

        if (connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
            connectionString.Contains("FileName=", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<FinanceDbContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            services.AddDbContext<FinanceDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();

        return services;
    }
}
