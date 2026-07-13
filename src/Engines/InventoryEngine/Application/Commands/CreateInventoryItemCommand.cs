using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.InventoryEngine.Application.Commands;

public record CreateInventoryItemCommand(string Sku, string Name, int Quantity, Guid WarehouseId, string? Description = null) : IRequest<Result<Guid>>;

public class CreateInventoryItemCommandHandler : IRequestHandler<CreateInventoryItemCommand, Result<Guid>>
{
    private readonly IInventoryRepository _repository;

    public CreateInventoryItemCommandHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateInventoryItemCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity < 0)
            return Result<Guid>.Failure(Error.Validation("Quantity cannot be negative."));

        var sku = Sku.Create(request.Sku);

        var item = InventoryItem.Create(sku, request.Name, request.Quantity, request.WarehouseId, request.Description);

        await _repository.AddAsync(item, cancellationToken);

        return Result<Guid>.Success(item.Id.Value);
    }
}
