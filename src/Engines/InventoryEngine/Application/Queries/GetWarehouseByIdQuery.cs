using Eleraki.InventoryEngine.Application.DTOs;
using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.InventoryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.InventoryEngine.Application.Queries;

public record GetWarehouseByIdQuery(Guid Id) : IRequest<WarehouseDto?>;

public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, WarehouseDto?>
{
    private readonly IWarehouseRepository _repository;

    public GetWarehouseByIdQueryHandler(IWarehouseRepository repository)
    {
        _repository = repository;
    }

    public async Task<WarehouseDto?> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
    {
        var warehouse = await _repository.GetByIdAsync(WarehouseId.From(request.Id), cancellationToken);

        if (warehouse is null)
            return null;

        return new WarehouseDto
        {
            Id = warehouse.Id.Value,
            Name = warehouse.Name,
            Code = warehouse.Code,
            Address = warehouse.Address,
            Status = warehouse.Status.ToString()
        };
    }
}
