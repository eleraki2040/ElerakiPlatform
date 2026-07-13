using Eleraki.SalesEngine.Domain.Customers;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using Eleraki.SalesEngine.Domain.SalesOrders;
using Eleraki.SalesEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.SalesEngine.Application.Commands;

public record CreateSalesOrderCommand(Guid CustomerId, string CustomerName, string OrderNumber) : IRequest<Result<Guid>>;

public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, Result<Guid>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ISalesOrderRepository _salesOrderRepository;

    public CreateSalesOrderCommandHandler(ICustomerRepository customerRepository, ISalesOrderRepository salesOrderRepository)
    {
        _customerRepository = customerRepository;
        _salesOrderRepository = salesOrderRepository;
    }

    public async Task<Result<Guid>> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(CustomerId.From(request.CustomerId), cancellationToken);
        if (customer is null)
            return Result<Guid>.Failure(Error.NotFound("Customer not found."));

        var existing = await _salesOrderRepository.ExistsByOrderNumberAsync(request.OrderNumber, cancellationToken);
        if (existing)
            return Result<Guid>.Failure(Error.Conflict("Sales order with this order number already exists."));

        var salesOrder = SalesOrder.Create(request.OrderNumber, CustomerId.From(request.CustomerId), request.CustomerName);

        await _salesOrderRepository.AddAsync(salesOrder, cancellationToken);

        return Result<Guid>.Success(salesOrder.Id.Value);
    }
}
