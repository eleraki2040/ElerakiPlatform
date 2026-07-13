using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.HREngine.Application.Commands;
using Eleraki.HREngine.Application.Validators;

namespace Eleraki.HREngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddHREngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommand).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
