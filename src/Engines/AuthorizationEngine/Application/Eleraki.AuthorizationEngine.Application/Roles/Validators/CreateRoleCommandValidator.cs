using FluentValidation;
using Eleraki.AuthorizationEngine.Application.Roles.Commands;

namespace Eleraki.AuthorizationEngine.Application.Roles.Validators;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Role name is required and must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Role description must not exceed 500 characters.");
    }
}
