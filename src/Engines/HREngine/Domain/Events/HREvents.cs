using Eleraki.HREngine.Domain;
using Eleraki.SharedKernel.Events;

namespace Eleraki.HREngine.Domain.Events;

public sealed record EmployeeCreatedDomainEvent(EmployeeId EmployeeId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record EmployeeUpdatedDomainEvent(EmployeeId EmployeeId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record EmployeeActivatedDomainEvent(EmployeeId EmployeeId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record EmployeeDeactivatedDomainEvent(EmployeeId EmployeeId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DepartmentCreatedDomainEvent(DepartmentId DepartmentId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DepartmentUpdatedDomainEvent(DepartmentId DepartmentId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DepartmentActivatedDomainEvent(DepartmentId DepartmentId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record DepartmentDeactivatedDomainEvent(DepartmentId DepartmentId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PositionCreatedDomainEvent(PositionId PositionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PositionUpdatedDomainEvent(PositionId PositionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PositionActivatedDomainEvent(PositionId PositionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record PositionDeactivatedDomainEvent(PositionId PositionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record SalaryCreatedDomainEvent(SalaryId SalaryId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record SalaryUpdatedDomainEvent(SalaryId SalaryId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record AttendanceRecordedDomainEvent(AttendanceId AttendanceId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record AttendanceCheckedOutDomainEvent(AttendanceId AttendanceId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record LeaveRequestedDomainEvent(LeaveId LeaveId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record LeaveApprovedDomainEvent(LeaveId LeaveId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record LeaveRejectedDomainEvent(LeaveId LeaveId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record LeaveCancelledDomainEvent(LeaveId LeaveId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
