using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.IdentityEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
