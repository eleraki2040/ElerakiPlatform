using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Eleraki.HospitalEngine.Application.Commands;
using Eleraki.HospitalEngine.Application.Validators;

namespace Eleraki.HospitalEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddHospitalEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreatePatientCommand).Assembly);
        });

        services.AddValidatorsFromAssemblyContaining<CreatePatientCommandValidator>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
