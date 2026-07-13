using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.DeliveryEngine.Application.Commands;
using Eleraki.DeliveryEngine.Application.Validators;

namespace Eleraki.DeliveryEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddDeliveryEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateDeliveryCommand).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateDeliveryValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
