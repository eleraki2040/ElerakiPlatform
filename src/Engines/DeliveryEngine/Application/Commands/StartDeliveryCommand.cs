using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.DeliveryEngine.Application.Commands;

public record StartDeliveryCommand(Guid DeliveryId) : IRequest<Result<Unit>>;

public class StartDeliveryCommandHandler : IRequestHandler<StartDeliveryCommand, Result<Unit>>
{
    private readonly IDeliveryRepository _repository;

    public StartDeliveryCommandHandler(IDeliveryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(StartDeliveryCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _repository.GetByIdAsync(request.DeliveryId, cancellationToken);
        if (delivery is null)
            return Result<Unit>.Failure(Error.NotFound("Delivery not found."));

        delivery.Start();
        await _repository.AddAsync(delivery, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
