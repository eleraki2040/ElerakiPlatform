using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Eleraki.AuthorizationEngine.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using System.Net;

namespace Eleraki.AuthorizationEngine.IntegrationTests;

/// <summary>
/// Custom WebApplicationFactory for integration testing with in-memory SQLite.
/// </summary>
public class AuthorizationWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly SqliteConnection _connection;

    public AuthorizationWebApplicationFactory()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AuthorizationDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });
        });
    }

    public async Task InitializeAsync()
    {
        using var scope = Services.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<AuthorizationDbContext>();
        db.Database.EnsureCreated();
        await Task.CompletedTask;
    }

    public new async Task DisposeAsync()
    {
        _connection.Dispose();
        await base.DisposeAsync();
    }
}
