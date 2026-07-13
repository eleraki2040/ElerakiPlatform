using Eleraki.InventoryEngine.Domain.Entities;
using Eleraki.InventoryEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.InventoryEngine.Application.Commands;

public record CreateWarehouseCommand(string Name, string Code, string? Address = null) : IRequest<Result<Guid>>;

public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Result<Guid>>
{
    private readonly IWarehouseRepository _repository;

    public CreateWarehouseCommandHandler(IWarehouseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = Warehouse.Create(request.Name, request.Code, request.Address);

        await _repository.AddAsync(warehouse, cancellationToken);

        return Result<Guid>.Success(warehouse.Id.Value);
    }
}
