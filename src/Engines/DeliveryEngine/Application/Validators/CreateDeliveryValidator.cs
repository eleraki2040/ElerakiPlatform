using Eleraki.DeliveryEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.DeliveryEngine.Application.Validators;

public class CreateDeliveryValidator : AbstractValidator<CreateDeliveryCommand>
{
    public CreateDeliveryValidator()
    {
        RuleFor(x => x.RecipientName)
            .NotEmpty().WithMessage("Recipient name is required.")
            .MaximumLength(200).WithMessage("Recipient name cannot exceed 200 characters.");

        RuleFor(x => x.DeliveryAddress)
            .NotEmpty().WithMessage("Delivery address is required.")
            .MaximumLength(500).WithMessage("Delivery address cannot exceed 500 characters.");

        RuleFor(x => x.ScheduledDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Scheduled date must be today or in the future.");

        RuleFor(x => x.Lines)
            .NotEmpty().WithMessage("Delivery must contain at least one line item.");

        RuleForEach(x => x.Lines).ChildRules(line =>
        {
            line.RuleFor(l => l.ProductDescription)
                .NotEmpty().WithMessage("Product description is required.");

            line.RuleFor(l => l.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            line.RuleFor(l => l.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price cannot be negative.");

            line.RuleFor(l => l.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be a 3-character code.");
        });

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters.");
    }
}
