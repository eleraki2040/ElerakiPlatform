using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.FinanceEngine.Application.Commands;
using Eleraki.FinanceEngine.Application.Validators;

namespace Eleraki.FinanceEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddFinanceEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommand).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateAccountCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
