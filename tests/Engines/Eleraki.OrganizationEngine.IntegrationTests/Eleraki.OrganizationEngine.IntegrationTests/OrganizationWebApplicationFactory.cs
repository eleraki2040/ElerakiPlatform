using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using FluentValidation;
using Eleraki.OrganizationEngine.Infrastructure.Persistence;
using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.OrganizationEngine.Infrastructure.Repositories;
using Eleraki.OrganizationEngine.Application.Validators;
using Xunit;

namespace Eleraki.OrganizationEngine.IntegrationTests;

public class OrganizationWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly SqliteConnection _connection;

    public OrganizationWebApplicationFactory()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<OrganizationDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<OrganizationDbContext>(options =>
                options.UseSqlite(_connection));

            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOrganizationUnitRepository, OrganizationUnitRepository>();
            services.AddScoped<IOrganizationUnitTypeRepository, OrganizationUnitTypeRepository>();

            services.AddValidatorsFromAssemblyContaining<CreateOrganizationCommandValidator>();
        });
    }

    public async Task InitializeAsync()
    {
        using var scope = Services.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<OrganizationDbContext>();
        db.Database.EnsureCreated();
        await Task.CompletedTask;
    }

    public new async Task DisposeAsync()
    {
        _connection.Dispose();
        await base.DisposeAsync();
    }
}
