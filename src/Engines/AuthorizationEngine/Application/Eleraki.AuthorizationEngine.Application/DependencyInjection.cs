using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.AuthorizationEngine.Application.Roles.Validators;

namespace Eleraki.AuthorizationEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthorizationEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateRoleCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
