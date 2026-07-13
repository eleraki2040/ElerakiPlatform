using FluentValidation;
using MediatR;

namespace Eleraki.SchoolManagementEngine.Application;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(typeof(TRequest));
        var validator = _serviceProvider.GetService(validatorType) as IValidator<TRequest>;
        if (validator is not null)
        {
            var context = new ValidationContext<TRequest>(request);
            var result = await validator.ValidateAsync(context, cancellationToken);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

        return await next();
    }
}
