using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using MediatR;
using Eleraki.HREngine.Application;
using Eleraki.HREngine.Infrastructure.Persistence;
using Eleraki.HREngine.Application.Validators;
using Eleraki.HREngine.Application.Commands;
using Xunit;

namespace Eleraki.HREngine.IntegrationTests;

public class HRWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
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

            services.AddDbContext<HRDbContext>(options =>
                options.UseInMemoryDatabase("HRTestDb"));

            services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return base.CreateHost(builder);
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
    }
}
