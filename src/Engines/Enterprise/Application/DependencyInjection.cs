using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Eleraki.Enterprise.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddEnterpriseEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateEnterpriseCommand).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreatePartyCommand).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(CreateEnterpriseCommand).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
