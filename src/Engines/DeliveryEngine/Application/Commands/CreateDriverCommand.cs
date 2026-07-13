using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;
using MediatR;

namespace Eleraki.DeliveryEngine.Application.Commands;

public record CreateDriverCommand(string FullName, string LicenseNumber, string Phone, string Email) : IRequest<Result<Guid>>;

public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, Result<Guid>>
{
    private readonly IDriverRepository _driverRepository;

    public CreateDriverCommandHandler(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<Result<Guid>> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var phone = PhoneNumber.Create(request.Phone);
        var email = Email.Create(request.Email);

        var driver = Driver.Create(request.FullName, request.LicenseNumber, phone, email);

        await _driverRepository.AddAsync(driver, cancellationToken);

        return Result<Guid>.Success(driver.Id.Value);
    }
}
