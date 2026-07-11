using FluentValidation;
using Eleraki.OrganizationEngine.Application.Commands;

namespace Eleraki.OrganizationEngine.Application.Validators;

/// <summary>
/// Validator for CreateOrganizationCommand.
/// </summary>
public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateOrganizationCommandValidator"/> class.
    /// </summary>
    public CreateOrganizationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Organization name is required and must not exceed 200 characters.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Organization code is required and must not exceed 50 characters.");
    }
}