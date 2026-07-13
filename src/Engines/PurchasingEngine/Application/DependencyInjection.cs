using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.PurchasingEngine.Application.Commands;
using Eleraki.PurchasingEngine.Application.Validators;

namespace Eleraki.PurchasingEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPurchasingEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreatePurchaseOrderCommand).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreatePurchaseOrderValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
