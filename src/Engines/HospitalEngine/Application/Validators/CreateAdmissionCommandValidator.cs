using Eleraki.HospitalEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.HospitalEngine.Application.Validators;

public class CreateAdmissionCommandValidator : AbstractValidator<CreateAdmissionCommand>
{
    public CreateAdmissionCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("Patient ID is required.");
    }
}
