using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.HREngine.Application.Commands;

public record UpdateEmployeeCommand(Guid Id, string FirstName, string LastName, string Email, string Phone, string DepartmentId, string PositionId, string? SalaryId = null) : IRequest<Result<Guid>>;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<Guid>>
{
    private readonly IEmployeeRepository _repository;

    public UpdateEmployeeCommandHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetByIdAsync(EmployeeId.From(request.Id), cancellationToken);
        if (employee is null)
            return Result<Guid>.Failure(Error.NotFound("Employee not found."));

        employee.Update(request.FirstName, request.LastName, request.Email, request.Phone, request.DepartmentId, request.PositionId, request.SalaryId);

        await _repository.UpdateAsync(employee, cancellationToken);

        return Result<Guid>.Success(employee.Id.Value);
    }
}
