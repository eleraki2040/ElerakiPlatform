using Eleraki.PurchasingEngine.Application.DTOs;
using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using MediatR;

namespace Eleraki.PurchasingEngine.Application.Queries;

public record GetVendorByIdQuery(Guid Id) : IRequest<VendorDto?>;

public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, VendorDto?>
{
    private readonly IVendorRepository _vendorRepository;

    public GetVendorByIdQueryHandler(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public async Task<VendorDto?> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        var vendor = await _vendorRepository.GetByIdAsync(VendorId.From(request.Id), cancellationToken);

        if (vendor is null)
            return null;

        return new VendorDto
        {
            Id = vendor.Id.Value,
            Name = vendor.Name,
            ContactEmail = vendor.ContactEmail,
            ContactPhone = vendor.ContactPhone,
            Address = vendor.Address,
            Status = vendor.Status.ToString()
        };
    }
}
