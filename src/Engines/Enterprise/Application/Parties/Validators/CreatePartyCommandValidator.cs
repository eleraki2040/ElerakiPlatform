using FluentValidation;
using Eleraki.Enterprise.Application.Parties.Commands;

namespace Eleraki.Enterprise.Application.Parties.Validators;

public class CreatePartyCommandValidator : AbstractValidator<CreatePartyCommand>
{
    public CreatePartyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Party name is required and must not exceed 200 characters.");
    }
}
