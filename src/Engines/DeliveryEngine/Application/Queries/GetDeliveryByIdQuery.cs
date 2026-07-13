using Eleraki.DeliveryEngine.Application.DTOs;
using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.DeliveryEngine.Application.Queries;

public record GetDeliveryByIdQuery(Guid Id) : IRequest<DeliveryDto?>;

public class GetDeliveryByIdQueryHandler : IRequestHandler<GetDeliveryByIdQuery, DeliveryDto?>
{
    private readonly IDeliveryRepository _repository;

    public GetDeliveryByIdQueryHandler(IDeliveryRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeliveryDto?> Handle(GetDeliveryByIdQuery request, CancellationToken cancellationToken)
    {
        var delivery = await _repository.GetByIdAsync(request.Id, cancellationToken);

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
