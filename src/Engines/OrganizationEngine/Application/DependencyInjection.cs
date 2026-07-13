using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.OrganizationEngine.Application.Commands;
using Eleraki.OrganizationEngine.Application.Validators;

namespace Eleraki.OrganizationEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddOrganizationEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateOrganizationCommand).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateOrganizationCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
