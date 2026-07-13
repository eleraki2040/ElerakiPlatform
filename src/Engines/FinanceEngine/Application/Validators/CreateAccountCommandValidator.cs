using Eleraki.FinanceEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.FinanceEngine.Application.Validators;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Account name is required.")
            .MaximumLength(200).WithMessage("Account name cannot exceed 200 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Account code is required.")
            .MaximumLength(20).WithMessage("Account code cannot exceed 20 characters.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid account type.");
    }
}
