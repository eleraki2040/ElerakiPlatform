using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.InventoryEngine.Application.Commands;
using Eleraki.InventoryEngine.Application.Validators;

namespace Eleraki.InventoryEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddInventoryEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateInventoryItemCommand).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateInventoryItemCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
