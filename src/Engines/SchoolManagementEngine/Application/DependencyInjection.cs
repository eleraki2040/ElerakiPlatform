using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.SchoolManagementEngine.Application.Commands;
using Eleraki.SchoolManagementEngine.Application.Validators;

namespace Eleraki.SchoolManagementEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddSchoolManagementEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateStudentCommand).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreateStudentCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
