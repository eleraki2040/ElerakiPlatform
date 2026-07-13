using Eleraki.InventoryEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.InventoryEngine.Application.Validators;

public class CreateInventoryItemCommandValidator : AbstractValidator<CreateInventoryItemCommand>
{
    public CreateInventoryItemCommandValidator()
    {
        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("SKU is required.")
            .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Item name is required.")
            .MaximumLength(200).WithMessage("Item name cannot exceed 200 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

        RuleFor(x => x.WarehouseId)
            .NotEqual(Guid.Empty).WithMessage("Warehouse ID is required.");
    }
}
