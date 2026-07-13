using Eleraki.DeliveryEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.DeliveryEngine.Application.Validators;

public class AssignDriverValidator : AbstractValidator<AssignDriverCommand>
{
    public AssignDriverValidator()
    {
        RuleFor(x => x.DeliveryId)
            .NotEmpty().WithMessage("Delivery ID is required.");

        RuleFor(x => x.DriverId)
            .NotEmpty().WithMessage("Driver ID is required.");
    }
}
