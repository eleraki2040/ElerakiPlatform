using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.SalesEngine.Application.Commands;

public record FulfillSalesOrderCommand(Guid SalesOrderId) : IRequest<Result<Guid>>;

public class FulfillSalesOrderCommandHandler : IRequestHandler<FulfillSalesOrderCommand, Result<Guid>>
{
    private readonly ISalesOrderRepository _salesOrderRepository;

    public FulfillSalesOrderCommandHandler(ISalesOrderRepository salesOrderRepository)
    {
        _salesOrderRepository = salesOrderRepository;
    }

    public async Task<Result<Guid>> Handle(FulfillSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var salesOrder = await _salesOrderRepository.GetByIdAsync(SalesOrderId.From(request.SalesOrderId), cancellationToken);
        if (salesOrder is null)
            return Result<Guid>.Failure(Error.NotFound("Sales order not found."));

        if (salesOrder.Status != SalesOrderStatus.Approved)
            return Result<Guid>.Failure(Error.Validation("Sales order must be approved before it can be fulfilled."));

        salesOrder.Fulfill();

        await _salesOrderRepository.UpdateAsync(salesOrder, cancellationToken);

        return Result<Guid>.Success(salesOrder.Id.Value);
    }
}
