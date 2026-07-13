using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using MediatR;
using Eleraki.PurchasingEngine.Application;
using Eleraki.PurchasingEngine.Infrastructure.Persistence;
using Eleraki.PurchasingEngine.Application.Validators;
using Eleraki.PurchasingEngine.Application.Commands;
using Xunit;

namespace Eleraki.PurchasingEngine.IntegrationTests;

public class PurchasingWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
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

            services.AddDbContext<PurchasingDbContext>(options =>
                options.UseInMemoryDatabase("PurchasingTestDb"));

            services.AddValidatorsFromAssemblyContaining<CreatePurchaseOrderValidator>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return base.CreateHost(builder);
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
    }
}
