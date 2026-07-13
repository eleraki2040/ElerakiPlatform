using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using MediatR;
using Eleraki.DeliveryEngine.Application;
using Eleraki.DeliveryEngine.Infrastructure.Persistence;
using Eleraki.DeliveryEngine.Application.Validators;
using Eleraki.DeliveryEngine.Application.Commands;
using Xunit;

namespace Eleraki.DeliveryEngine.IntegrationTests;

public class DeliveryWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
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

            services.AddDbContext<DeliveryDbContext>(options =>
                options.UseInMemoryDatabase("DeliveryTestDb"));

            services.AddValidatorsFromAssemblyContaining<CreateDeliveryValidator>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return base.CreateHost(builder);
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
    }
}
