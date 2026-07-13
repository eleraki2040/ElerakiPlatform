using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using MediatR;
using Eleraki.SalesEngine.Application;
using Eleraki.SalesEngine.Infrastructure.Persistence;
using Eleraki.SalesEngine.Application.Validators;
using Eleraki.SalesEngine.Application.Commands;
using Xunit;

namespace Eleraki.SalesEngine.IntegrationTests;

public class SalesWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptors = services
                .Where(d => d.ServiceType.FullName != null &&
                           (d.ServiceType.FullName.Contains("DbContextOptions") ||
                            d.ImplementationType?.Namespace?.Contains("EntityFrameworkCore") == true))
                .ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<SalesDbContext>(options =>
                options.UseInMemoryDatabase("SalesTestDb"));

            services.AddValidatorsFromAssemblyContaining<CreateSalesOrderValidator>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return base.CreateHost(builder);
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
    }
}
