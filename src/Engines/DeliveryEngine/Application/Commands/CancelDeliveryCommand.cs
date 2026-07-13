using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.DeliveryEngine.Application.Commands;

public record CancelDeliveryCommand(Guid DeliveryId, string? Reason = null) : IRequest<Result<Unit>>;

public class CancelDeliveryCommandHandler : IRequestHandler<CancelDeliveryCommand, Result<Unit>>
{
    private readonly IDeliveryRepository _repository;

    public CancelDeliveryCommandHandler(IDeliveryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(CancelDeliveryCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _repository.GetByIdAsync(request.DeliveryId, cancellationToken);
        if (delivery is null)
            return Result<Unit>.Failure(Error.NotFound("Delivery not found."));

        delivery.Cancel(request.Reason);
        await _repository.AddAsync(delivery, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
