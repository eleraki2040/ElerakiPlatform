using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using MediatR;
using Eleraki.HospitalEngine.Application;
using Eleraki.HospitalEngine.Infrastructure.Persistence;
using Eleraki.HospitalEngine.Application.Validators;
using Eleraki.HospitalEngine.Application.Commands;
using Xunit;

namespace Eleraki.HospitalEngine.IntegrationTests;

public class HospitalWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
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

            services.AddDbContext<HospitalDbContext>(options =>
                options.UseInMemoryDatabase("HospitalTestDb"));

            services.AddValidatorsFromAssemblyContaining<CreatePatientCommandValidator>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return base.CreateHost(builder);
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
    }
}
