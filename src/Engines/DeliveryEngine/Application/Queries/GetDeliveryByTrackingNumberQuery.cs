using Eleraki.DeliveryEngine.Application.DTOs;
using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.DeliveryEngine.Application.Queries;

public record GetDeliveryByTrackingNumberQuery(string TrackingNumber) : IRequest<DeliveryDto?>;

public class GetDeliveryByTrackingNumberQueryHandler : IRequestHandler<GetDeliveryByTrackingNumberQuery, DeliveryDto?>
{
    private readonly IDeliveryRepository _repository;

    public GetDeliveryByTrackingNumberQueryHandler(IDeliveryRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeliveryDto?> Handle(GetDeliveryByTrackingNumberQuery request, CancellationToken cancellationToken)
    {
        var trackingNumber = TrackingNumber.From(request.TrackingNumber);
        var delivery = await _repository.GetByTrackingNumberAsync(trackingNumber, cancellationToken);

        if (delivery is null)
            return null;

        return new DeliveryDto
        {
            Id = delivery.Id,
            TrackingNumber = delivery.TrackingNumber.Value,
            RecipientName = delivery.RecipientName,
            DeliveryAddress = delivery.DeliveryAddress,
            Status = delivery.Status.ToString(),
            ScheduledDate = delivery.ScheduledDate,
            DeliveredAt = delivery.DeliveredAt,
            Notes = delivery.Notes,
            TotalAmount = delivery.TotalAmount.Amount,
            Lines = delivery.Lines.Select(l => new DeliveryLineDto
            {
                Id = l.Id,
                ProductDescription = l.ProductDescription,
                Quantity = l.Quantity.Value,
                UnitPrice = l.UnitPrice.Amount,
                LineTotal = l.LineTotal.Amount
            }).ToList()
        };
    }
}
