using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.HREngine.Application.Commands;

public record CreateEmployeeCommand(string FirstName, string LastName, string Email, string Phone, DateTime DateOfBirth, string DepartmentId, string PositionId, string? SalaryId = null) : IRequest<Result<Guid>>;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<Guid>>
{
    private readonly IEmployeeRepository _repository;

    public CreateEmployeeCommandHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = Employee.Create(request.FirstName, request.LastName, request.Email, request.Phone, request.DateOfBirth, request.DepartmentId, request.PositionId, request.SalaryId);

        await _repository.AddAsync(employee, cancellationToken);

        return Result<Guid>.Success(employee.Id.Value);
    }
}
