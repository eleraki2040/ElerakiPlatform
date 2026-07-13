using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.PurchasingEngine.Application.Commands;

public record AddPurchaseOrderLineCommand(Guid PurchaseOrderId, string ProductName, int Quantity, decimal UnitPrice, string Currency) : IRequest<Result<Guid>>;

public class AddPurchaseOrderLineCommandHandler : IRequestHandler<AddPurchaseOrderLineCommand, Result<Guid>>
{
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;

    public AddPurchaseOrderLineCommandHandler(IPurchaseOrderRepository purchaseOrderRepository)
    {
        _purchaseOrderRepository = purchaseOrderRepository;
    }

    public async Task<Result<Guid>> Handle(AddPurchaseOrderLineCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity <= 0)
            return Result<Guid>.Failure(Error.Validation("Quantity must be greater than zero."));

        if (request.UnitPrice <= 0)
            return Result<Guid>.Failure(Error.Validation("Unit price must be greater than zero."));

        var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(PurchaseOrderId.From(request.PurchaseOrderId), cancellationToken);
        if (purchaseOrder is null)
            return Result<Guid>.Failure(Error.NotFound("Purchase order not found."));

        var lineId = PurchaseOrderLineId.New();
        var unitPrice = Money.Create(request.UnitPrice, request.Currency);

        purchaseOrder.AddLine(lineId, request.ProductName, request.Quantity, unitPrice);

        await _purchaseOrderRepository.UpdateAsync(purchaseOrder, cancellationToken);

        return Result<Guid>.Success(lineId.Value);
    }
}
