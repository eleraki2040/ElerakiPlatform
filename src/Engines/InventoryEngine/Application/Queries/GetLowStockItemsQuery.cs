using Eleraki.InventoryEngine.Application.DTOs;
using Eleraki.InventoryEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.InventoryEngine.Application.Queries;

public record GetLowStockItemsQuery(int Threshold) : IRequest<List<InventoryItemDto>>;

public class GetLowStockItemsQueryHandler : IRequestHandler<GetLowStockItemsQuery, List<InventoryItemDto>>
{
    private readonly IInventoryRepository _repository;

    public GetLowStockItemsQueryHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<InventoryItemDto>> Handle(GetLowStockItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetLowStockAsync(request.Threshold, cancellationToken);

        return items.Select(item => new InventoryItemDto
        {
            Id = item.Id.Value,
            Sku = item.Sku.Value,
            Name = item.Name,
            Description = item.Description,
            Quantity = item.Quantity.Value,
            Location = item.Location?.ToString(),
            WarehouseId = item.WarehouseId,
            Status = item.Status.ToString()
        }).ToList();
    }
}
