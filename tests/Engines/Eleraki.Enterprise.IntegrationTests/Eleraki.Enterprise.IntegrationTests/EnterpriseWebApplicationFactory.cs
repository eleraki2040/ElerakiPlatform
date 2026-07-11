using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Eleraki.Enterprise.Infrastructure.Persistence;

namespace Eleraki.Enterprise.IntegrationTests;

/// <summary>
/// Custom WebApplicationFactory for integration testing with in-memory SQLite.
/// </summary>
public class EnterpriseWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly SqliteConnection _connection;

    public EnterpriseWebApplicationFactory()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<EnterpriseDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<EnterpriseDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });
        });
    }

    public async Task InitializeAsync()
    {
        using var scope = Services.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EnterpriseDbContext>();
        db.Database.EnsureCreated();
        await Task.CompletedTask;
    }

    public new async Task DisposeAsync()
    {
        _connection.Dispose();
        await base.DisposeAsync();
    }
}
