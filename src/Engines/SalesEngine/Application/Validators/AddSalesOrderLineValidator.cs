using Eleraki.SalesEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.SalesEngine.Application.Validators;

public class AddSalesOrderLineValidator : AbstractValidator<AddSalesOrderLineCommand>
{
    public AddSalesOrderLineValidator()
    {
        RuleFor(x => x.SalesOrderId)
            .NotEmpty().WithMessage("Sales order ID is required.");

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Unit price cannot be negative.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .Length(3).WithMessage("Currency must be a 3-character code.");
    }
}
