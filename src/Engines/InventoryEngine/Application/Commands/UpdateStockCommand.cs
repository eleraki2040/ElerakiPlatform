using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.InventoryEngine.Application.Commands;

public record UpdateStockCommand(Guid InventoryItemId, int Quantity) : IRequest<Result<Guid>>;

public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Result<Guid>>
{
    private readonly IInventoryRepository _repository;

    public UpdateStockCommandHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(InventoryItemId.From(request.InventoryItemId), cancellationToken);

        if (item is null)
            return Result<Guid>.Failure(Error.NotFound("Inventory item not found."));

        item.UpdateStock(request.Quantity);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(item.Id.Value);
    }
}
