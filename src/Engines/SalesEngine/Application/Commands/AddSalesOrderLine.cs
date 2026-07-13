using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SalesEngine.Domain.SalesOrderLines;
using Eleraki.SalesEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.SalesEngine.Application.Commands;

public record AddSalesOrderLineCommand(Guid SalesOrderId, string ProductName, int Quantity, decimal UnitPrice, string Currency) : IRequest<Result<Guid>>;

public class AddSalesOrderLineCommandHandler : IRequestHandler<AddSalesOrderLineCommand, Result<Guid>>
{
    private readonly ISalesOrderRepository _salesOrderRepository;

    public AddSalesOrderLineCommandHandler(ISalesOrderRepository salesOrderRepository)
    {
        _salesOrderRepository = salesOrderRepository;
    }

    public async Task<Result<Guid>> Handle(AddSalesOrderLineCommand request, CancellationToken cancellationToken)
    {
        var salesOrder = await _salesOrderRepository.GetByIdAsync(SalesOrderId.From(request.SalesOrderId), cancellationToken);
        if (salesOrder is null)
            return Result<Guid>.Failure(Error.NotFound("Sales order not found."));

        if (salesOrder.Status != SalesOrderStatus.Draft)
            return Result<Guid>.Failure(Error.Validation("Cannot add lines to a sales order that is not in Draft status."));

        var unitPrice = Money.Create(request.UnitPrice, request.Currency);
        var line = SalesOrderLine.Create(salesOrder.Id, request.ProductName, request.Quantity, unitPrice);

        salesOrder.AddLine(line);

        await _salesOrderRepository.UpdateAsync(salesOrder, cancellationToken);

        return Result<Guid>.Success(line.Id.Value);
    }
}
