using FluentValidation;
using Eleraki.SchoolManagementEngine.Application.Commands;

namespace Eleraki.SchoolManagementEngine.Application.Validators;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(500);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
    }
}
