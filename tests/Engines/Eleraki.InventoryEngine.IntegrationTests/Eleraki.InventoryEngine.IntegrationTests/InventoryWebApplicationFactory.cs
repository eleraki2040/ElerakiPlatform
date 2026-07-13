using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using MediatR;
using Eleraki.InventoryEngine.Application;
using Eleraki.InventoryEngine.Infrastructure.Persistence;
using Eleraki.InventoryEngine.Application.Validators;
using Eleraki.InventoryEngine.Application.Commands;
using Xunit;

namespace Eleraki.InventoryEngine.IntegrationTests;

public class InventoryWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
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

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseInMemoryDatabase("InventoryTestDb"));

            services.AddValidatorsFromAssemblyContaining<CreateInventoryItemCommandValidator>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return base.CreateHost(builder);
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
    }
}
