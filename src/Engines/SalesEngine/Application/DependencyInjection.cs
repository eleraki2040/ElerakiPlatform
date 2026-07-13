using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.SalesEngine.Application.Commands;
using Eleraki.SalesEngine.Application.Validators;

namespace Eleraki.SalesEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddSalesEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateSalesOrderCommand).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateSalesOrderValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
