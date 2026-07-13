using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.WorkflowEngine.Application.Workflows.Validators;

namespace Eleraki.WorkflowEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkflowEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateWorkflowCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
