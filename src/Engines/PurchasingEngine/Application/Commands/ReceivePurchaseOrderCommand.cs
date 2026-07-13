using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.PurchasingEngine.Application.Commands;

public record ReceivePurchaseOrderCommand(Guid PurchaseOrderId) : IRequest<Result<Guid>>;

public class ReceivePurchaseOrderCommandHandler : IRequestHandler<ReceivePurchaseOrderCommand, Result<Guid>>
{
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;

    public ReceivePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository)
    {
        _purchaseOrderRepository = purchaseOrderRepository;
    }

    public async Task<Result<Guid>> Handle(ReceivePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(PurchaseOrderId.From(request.PurchaseOrderId), cancellationToken);
        if (purchaseOrder is null)
            return Result<Guid>.Failure(Error.NotFound("Purchase order not found."));

        purchaseOrder.Receive();

        await _purchaseOrderRepository.UpdateAsync(purchaseOrder, cancellationToken);

        return Result<Guid>.Success(purchaseOrder.Id.Value);
    }
}
