using FluentValidation;
using Eleraki.SchoolManagementEngine.Application.Commands;

namespace Eleraki.SchoolManagementEngine.Application.Validators;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.Credits).GreaterThan(0);
        RuleFor(x => x.TeacherId).NotEmpty();
    }
}
