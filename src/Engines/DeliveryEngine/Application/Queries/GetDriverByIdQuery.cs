using Eleraki.DeliveryEngine.Application.DTOs;
using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.DeliveryEngine.Application.Queries;

public record GetDriverByIdQuery(Guid Id) : IRequest<DriverDto?>;

public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, DriverDto?>
{
    private readonly IDriverRepository _driverRepository;

    public GetDriverByIdQueryHandler(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<DriverDto?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(DriverId.From(request.Id), cancellationToken);

        if (driver is null)
            return null;

        return new DriverDto
        {
            Id = driver.Id.Value,
            FullName = driver.FullName,
            LicenseNumber = driver.LicenseNumber,
            Phone = driver.Phone.Value,
            Email = driver.Email.Value,
            Status = driver.Status.ToString()
        };
    }
}
