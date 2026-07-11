using FluentValidation;
using Eleraki.Enterprise.Application.Commands;

namespace Eleraki.Enterprise.Application.Validators;

public class CreateEnterpriseCommandValidator : AbstractValidator<CreateEnterpriseCommand>
{
    public CreateEnterpriseCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Enterprise code is required and must not exceed 50 characters.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Enterprise name is required and must not exceed 200 characters.");

        RuleFor(x => x.LegalName)
            .MaximumLength(200)
            .WithMessage("Legal name must not exceed 200 characters.");
    }
}
