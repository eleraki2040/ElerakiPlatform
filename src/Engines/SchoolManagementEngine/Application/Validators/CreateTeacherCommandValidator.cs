using FluentValidation;
using Eleraki.SchoolManagementEngine.Application.Commands;

namespace Eleraki.SchoolManagementEngine.Application.Validators;

public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
{
    public CreateTeacherCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Specialization).NotEmpty().MaximumLength(200);
    }
}
