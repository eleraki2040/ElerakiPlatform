using Eleraki.SalesEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.SalesEngine.Application.Validators;

public class CreateSalesOrderValidator : AbstractValidator<CreateSalesOrderCommand>
{
    public CreateSalesOrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(200).WithMessage("Customer name cannot exceed 200 characters.");

        RuleFor(x => x.OrderNumber)
            .NotEmpty().WithMessage("Order number is required.")
            .MaximumLength(50).WithMessage("Order number cannot exceed 50 characters.");
    }
}
