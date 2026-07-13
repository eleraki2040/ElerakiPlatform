using Eleraki.HREngine.Application.Commands;
using FluentValidation;

namespace Eleraki.HREngine.Application.Validators;

public class RequestLeaveCommandValidator : AbstractValidator<RequestLeaveCommand>
{
    public RequestLeaveCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid leave type.");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate).WithMessage("Start date must be before or equal to end date.");
    }
}
