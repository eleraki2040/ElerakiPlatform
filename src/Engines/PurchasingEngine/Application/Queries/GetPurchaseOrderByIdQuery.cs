using Eleraki.PurchasingEngine.Application.DTOs;
using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using MediatR;

namespace Eleraki.PurchasingEngine.Application.Queries;

public record GetPurchaseOrderByIdQuery(Guid Id) : IRequest<PurchaseOrderDto?>;

public class GetPurchaseOrderByIdQueryHandler : IRequestHandler<GetPurchaseOrderByIdQuery, PurchaseOrderDto?>
{
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;

    public GetPurchaseOrderByIdQueryHandler(IPurchaseOrderRepository purchaseOrderRepository)
    {
        _purchaseOrderRepository = purchaseOrderRepository;
    }

    public async Task<PurchaseOrderDto?> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _purchaseOrderRepository.GetByIdAsync(PurchaseOrderId.From(request.Id), cancellationToken);

        if (order is null)
            return null;

        return new PurchaseOrderDto
        {
            Id = order.Id.Value,
            VendorId = order.VendorId.Value,
            Status = order.Status.ToString(),
            OrderDate = order.OrderDate,
            ExpectedDeliveryDate = order.ExpectedDeliveryDate,
            TotalAmount = order.TotalAmount.Amount,
            Currency = order.TotalAmount.Currency,
            Notes = order.Notes,
            Lines = order.Lines.Select(l => new PurchaseOrderLineDto
            {
                Id = l.Id.Value,
                ProductName = l.ProductName,
                Quantity = l.Quantity.Value,
                UnitPrice = l.UnitPrice.Amount,
                Currency = l.UnitPrice.Currency,
                LineTotal = l.LineTotal.Amount
            }).ToList()
        };
    }
}
