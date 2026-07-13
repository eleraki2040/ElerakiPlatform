using Eleraki.SalesEngine.Application.DTOs;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Domain.SalesOrders;
using MediatR;

namespace Eleraki.SalesEngine.Application.Queries;

public record GetSalesOrderByIdQuery(Guid Id) : IRequest<SalesOrderDto?>;

public class GetSalesOrderByIdQueryHandler : IRequestHandler<GetSalesOrderByIdQuery, SalesOrderDto?>
{
    private readonly ISalesOrderRepository _salesOrderRepository;

    public GetSalesOrderByIdQueryHandler(ISalesOrderRepository salesOrderRepository)
    {
        _salesOrderRepository = salesOrderRepository;
    }

    public async Task<SalesOrderDto?> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var salesOrder = await _salesOrderRepository.GetByIdAsync(SalesOrderId.From(request.Id), cancellationToken);

        if (salesOrder is null)
            return null;

        return new SalesOrderDto
        {
            Id = salesOrder.Id.Value,
            OrderNumber = salesOrder.OrderNumber,
            CustomerId = salesOrder.CustomerId.Value,
            CustomerName = salesOrder.CustomerName,
            OrderDate = salesOrder.OrderDate,
            Status = salesOrder.Status.ToString(),
            TotalAmount = salesOrder.TotalAmount.Amount,
            Lines = salesOrder.Lines.Select(line => new SalesOrderLineDto
            {
                Id = line.Id.Value,
                ProductName = line.ProductName,
                Quantity = line.Quantity.Value,
                UnitPrice = line.UnitPrice.Amount,
                TotalPrice = line.TotalPrice.Amount
            }).ToList()
        };
    }
}
