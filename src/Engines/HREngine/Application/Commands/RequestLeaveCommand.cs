using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.HREngine.Application.Commands;

public record RequestLeaveCommand(string EmployeeId, LeaveType Type, DateTime StartDate, DateTime EndDate, string? Reason = null) : IRequest<Result<Guid>>;

public class RequestLeaveCommandHandler : IRequestHandler<RequestLeaveCommand, Result<Guid>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILeaveRepository _leaveRepository;

    public RequestLeaveCommandHandler(IEmployeeRepository employeeRepository, ILeaveRepository leaveRepository)
    {
        _employeeRepository = employeeRepository;
        _leaveRepository = leaveRepository;
    }

    public async Task<Result<Guid>> Handle(RequestLeaveCommand request, CancellationToken cancellationToken)
    {
        var employeeId = Guid.Parse(request.EmployeeId);
        var employee = await _employeeRepository.GetByIdAsync(EmployeeId.From(employeeId), cancellationToken);
        if (employee is null)
            return Result<Guid>.Failure(Error.NotFound("Employee not found."));

        var leave = Leave.Request(request.EmployeeId, request.Type, request.StartDate, request.EndDate, request.Reason);

        await _leaveRepository.AddAsync(leave, cancellationToken);

        return Result<Guid>.Success(leave.Id.Value);
    }
}
