using Eleraki.OrganizationEngine.Application.Commands;
using FluentValidation;

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
            .NotEmpty().WithMessage("Organization name is required.")
            .MaximumLength(200).WithMessage("Organization name cannot exceed 200 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Organization code is required.")
            .MaximumLength(50).WithMessage("Organization code cannot exceed 50 characters.");
    }
}
