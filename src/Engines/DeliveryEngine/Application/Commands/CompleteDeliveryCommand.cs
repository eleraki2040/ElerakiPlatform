using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.DeliveryEngine.Application.Commands;

public record CompleteDeliveryCommand(Guid DeliveryId) : IRequest<Result<Unit>>;

public class CompleteDeliveryCommandHandler : IRequestHandler<CompleteDeliveryCommand, Result<Unit>>
{
    private readonly IDeliveryRepository _repository;

    public CompleteDeliveryCommandHandler(IDeliveryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(CompleteDeliveryCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _repository.GetByIdAsync(request.DeliveryId, cancellationToken);
        if (delivery is null)
            return Result<Unit>.Failure(Error.NotFound("Delivery not found."));

        delivery.Complete();
        await _repository.AddAsync(delivery, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
