using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.SalesEngine.Application.Commands;

public record ApproveSalesOrderCommand(Guid SalesOrderId) : IRequest<Result<Guid>>;

public class ApproveSalesOrderCommandHandler : IRequestHandler<ApproveSalesOrderCommand, Result<Guid>>
{
    private readonly ISalesOrderRepository _salesOrderRepository;

    public ApproveSalesOrderCommandHandler(ISalesOrderRepository salesOrderRepository)
    {
        _salesOrderRepository = salesOrderRepository;
    }

    public async Task<Result<Guid>> Handle(ApproveSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var salesOrder = await _salesOrderRepository.GetByIdAsync(SalesOrderId.From(request.SalesOrderId), cancellationToken);
        if (salesOrder is null)
            return Result<Guid>.Failure(Error.NotFound("Sales order not found."));

        salesOrder.Approve();

        await _salesOrderRepository.UpdateAsync(salesOrder, cancellationToken);

        return Result<Guid>.Success(salesOrder.Id.Value);
    }
}
