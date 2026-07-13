using Eleraki.InventoryEngine.Application.DTOs;
using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using MediatR;

namespace Eleraki.InventoryEngine.Application.Queries;

public record GetInventoryItemByIdQuery(Guid Id) : IRequest<InventoryItemDto?>;

public class GetInventoryItemByIdQueryHandler : IRequestHandler<GetInventoryItemByIdQuery, InventoryItemDto?>
{
    private readonly IInventoryRepository _repository;

    public GetInventoryItemByIdQueryHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<InventoryItemDto?> Handle(GetInventoryItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(InventoryItemId.From(request.Id), cancellationToken);

        if (item is null)
            return null;

        return new InventoryItemDto
        {
            Id = item.Id.Value,
            Sku = item.Sku.Value,
            Name = item.Name,
            Description = item.Description,
            Quantity = item.Quantity.Value,
            Location = item.Location?.ToString(),
            WarehouseId = item.WarehouseId,
            Status = item.Status.ToString()
        };
    }
}
