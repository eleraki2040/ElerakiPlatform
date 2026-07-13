using Eleraki.HREngine.Application.DTOs;
using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using MediatR;

namespace Eleraki.HREngine.Application.Queries;

public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto?>;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetByIdAsync(EmployeeId.From(request.Id), cancellationToken);

        if (employee is null)
            return null;

        return new EmployeeDto
        {
            Id = employee.Id.Value,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            DateOfBirth = employee.DateOfBirth,
            DepartmentId = employee.DepartmentId,
            PositionId = employee.PositionId,
            SalaryId = employee.SalaryId,
            Status = employee.Status.ToString()
        };
    }
}
