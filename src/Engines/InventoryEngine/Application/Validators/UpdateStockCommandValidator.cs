using Eleraki.InventoryEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.InventoryEngine.Application.Validators;

public class UpdateStockCommandValidator : AbstractValidator<UpdateStockCommand>
{
    public UpdateStockCommandValidator()
    {
        RuleFor(x => x.InventoryItemId)
            .NotEqual(Guid.Empty).WithMessage("Inventory item ID is required.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");
    }
}
