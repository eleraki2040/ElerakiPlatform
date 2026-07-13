using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.HREngine.Application.Commands;

public record RecordAttendanceCommand(string EmployeeId, DateTime AttendanceDate, DateTime? CheckInTime = null, string? Notes = null) : IRequest<Result<Guid>>;

public class RecordAttendanceCommandHandler : IRequestHandler<RecordAttendanceCommand, Result<Guid>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAttendanceRepository _attendanceRepository;

    public RecordAttendanceCommandHandler(IEmployeeRepository employeeRepository, IAttendanceRepository attendanceRepository)
    {
        _employeeRepository = employeeRepository;
        _attendanceRepository = attendanceRepository;
    }

    public async Task<Result<Guid>> Handle(RecordAttendanceCommand request, CancellationToken cancellationToken)
    {
        var employeeId = Guid.Parse(request.EmployeeId);
        var employee = await _employeeRepository.GetByIdAsync(EmployeeId.From(employeeId), cancellationToken);
        if (employee is null)
            return Result<Guid>.Failure(Error.NotFound("Employee not found."));

        var attendance = Attendance.Record(request.EmployeeId, request.AttendanceDate, request.CheckInTime, request.Notes);

        await _attendanceRepository.AddAsync(attendance, cancellationToken);

        return Result<Guid>.Success(attendance.Id.Value);
    }
}
