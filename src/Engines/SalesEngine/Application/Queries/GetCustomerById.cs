using Eleraki.SalesEngine.Application.DTOs;
using Eleraki.SalesEngine.Domain.Customers;
using Eleraki.SalesEngine.Domain.Identity;
using Eleraki.SalesEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.SalesEngine.Application.Queries;

public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto?>;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(CustomerId.From(request.Id), cancellationToken);

        if (customer is null)
            return null;

        return new CustomerDto
        {
            Id = customer.Id.Value,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address,
            Status = customer.Status.ToString()
        };
    }
}
