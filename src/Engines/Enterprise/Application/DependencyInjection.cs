using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.Enterprise.Application.Commands;
using Eleraki.Enterprise.Application.Parties.Commands;
using Eleraki.Enterprise.Application.Validators;

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

        services.AddValidatorsFromAssemblyContaining<CreateEnterpriseCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}