using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.OrganizationEngine.Application.Commands;

namespace Eleraki.OrganizationEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddOrganizationEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateOrganizationCommand).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(CreateOrganizationCommand).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
