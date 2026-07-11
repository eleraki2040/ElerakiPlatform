using FluentValidation;
using Eleraki.AuthorizationEngine.Application.Permissions.Commands;

namespace Eleraki.AuthorizationEngine.Application.Permissions.Validators;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Permission name is required and must not exceed 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Permission code is required and must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Permission description must not exceed 500 characters.");

        RuleFor(x => x.Resource)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Resource is required and must not exceed 100 characters.");

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Permission type is required.");
    }
}
