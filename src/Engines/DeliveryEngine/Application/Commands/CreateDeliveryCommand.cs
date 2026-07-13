using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.DeliveryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.DeliveryEngine.Application.Commands;

public record CreateDeliveryCommand(string RecipientName, string DeliveryAddress, DateTime ScheduledDate, List<DeliveryLineItem> Lines, string? Notes = null) : IRequest<Result<Guid>>;

public record DeliveryLineItem(string ProductDescription, decimal Quantity, decimal UnitPrice, string Currency);

public class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand, Result<Guid>>
{
    private readonly IDeliveryRepository _repository;

    public CreateDeliveryCommandHandler(IDeliveryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
    {
        var trackingNumber = TrackingNumber.New();
        var delivery = Delivery.Create(trackingNumber, request.RecipientName, request.DeliveryAddress, request.ScheduledDate, request.Notes);

        foreach (var line in request.Lines)
        {
            var quantity = Quantity.Create(line.Quantity);
            var unitPrice = Money.Create(line.UnitPrice, line.Currency);
            delivery.AddLine(line.ProductDescription, quantity, unitPrice);
        }

        await _repository.AddAsync(delivery, cancellationToken);

        return Result<Guid>.Success(delivery.Id);
    }
}
