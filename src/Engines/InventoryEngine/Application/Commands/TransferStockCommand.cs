using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.InventoryEngine.Application.Commands;

public record TransferStockCommand(Guid InventoryItemId, Guid ToWarehouseId) : IRequest<Result<Guid>>;

public class TransferStockCommandHandler : IRequestHandler<TransferStockCommand, Result<Guid>>
{
    private readonly IInventoryRepository _repository;

    public TransferStockCommandHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(TransferStockCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(InventoryItemId.From(request.InventoryItemId), cancellationToken);

        if (item is null)
            return Result<Guid>.Failure(Error.NotFound("Inventory item not found."));

        item.Transfer(request.ToWarehouseId);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(item.Id.Value);
    }
}
