using Eleraki.HospitalEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.HospitalEngine.Application.Validators;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Patient name is required.")
            .MaximumLength(200).WithMessage("Patient name cannot exceed 200 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");
    }
}
