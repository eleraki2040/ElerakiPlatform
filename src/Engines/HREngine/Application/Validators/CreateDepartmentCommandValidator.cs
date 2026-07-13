using Eleraki.HREngine.Application.Commands;
using FluentValidation;

namespace Eleraki.HREngine.Application.Validators;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(200).WithMessage("Department name cannot exceed 200 characters.");
    }
}
