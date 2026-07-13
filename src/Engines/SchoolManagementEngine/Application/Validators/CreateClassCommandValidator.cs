using FluentValidation;
using Eleraki.SchoolManagementEngine.Application.Commands;

namespace Eleraki.SchoolManagementEngine.Application.Validators;

public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
{
    public CreateClassCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Grade).NotEmpty().MaximumLength(50);
        RuleFor(x => x.HomeroomTeacherId).NotEmpty();
        RuleFor(x => x.MaxCapacity).GreaterThan(0);
    }
}
