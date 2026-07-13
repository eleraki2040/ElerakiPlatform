using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.PurchasingEngine.Application.Commands;

public record CreatePurchaseOrderCommand(Guid VendorId, DateTime? ExpectedDeliveryDate = null, string? Notes = null) : IRequest<Result<Guid>>;

public class CreatePurchaseOrderCommandHandler : IRequestHandler<CreatePurchaseOrderCommand, Result<Guid>>
{
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    private readonly IVendorRepository _vendorRepository;

    public CreatePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository, IVendorRepository vendorRepository)
    {
        _purchaseOrderRepository = purchaseOrderRepository;
        _vendorRepository = vendorRepository;
    }

    public async Task<Result<Guid>> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        var vendor = await _vendorRepository.GetByIdAsync(VendorId.From(request.VendorId), cancellationToken);
        if (vendor is null)
            return Result<Guid>.Failure(Error.Validation("Vendor not found."));

        var purchaseOrder = PurchaseOrder.Create(VendorId.From(request.VendorId), request.ExpectedDeliveryDate, request.Notes);

        await _purchaseOrderRepository.AddAsync(purchaseOrder, cancellationToken);

        return Result<Guid>.Success(purchaseOrder.Id.Value);
    }
}
