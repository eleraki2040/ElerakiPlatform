using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.DeliveryEngine.Application.Commands;

public record AssignDriverCommand(Guid DeliveryId, Guid DriverId) : IRequest<Result<Unit>>;

public class AssignDriverCommandHandler : IRequestHandler<AssignDriverCommand, Result<Unit>>
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IDriverRepository _driverRepository;

    public AssignDriverCommandHandler(IDeliveryRepository deliveryRepository, IDriverRepository driverRepository)
    {
        _deliveryRepository = deliveryRepository;
        _driverRepository = driverRepository;
    }

    public async Task<Result<Unit>> Handle(AssignDriverCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(request.DeliveryId, cancellationToken);
        if (delivery is null)
            return Result<Unit>.Failure(Error.NotFound("Delivery not found."));

        var driver = await _driverRepository.GetByIdAsync(DriverId.From(request.DriverId), cancellationToken);
        if (driver is null)
            return Result<Unit>.Failure(Error.NotFound("Driver not found."));

        delivery.AssignDriver(DriverId.From(request.DriverId));
        await _deliveryRepository.AddAsync(delivery, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
