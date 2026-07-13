using Eleraki.HREngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.HREngine.Domain;

public sealed class Leave : AggregateRoot<LeaveId>
{
    public string EmployeeId { get; private set; } = string.Empty;
    public LeaveType Type { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string? Reason { get; private set; }
    public LeaveStatus Status { get; private set; }
    public string? ApprovedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Leave(LeaveId id) : base(id)
    {
    }

    public static Leave Request(string employeeId, LeaveType type, DateTime startDate, DateTime endDate, string? reason = null)
    {
        Guard.NotNullOrEmpty(employeeId, nameof(employeeId));
        Guard.Ensure(Enum.IsDefined(typeof(LeaveType), type), $"{nameof(type)} is not a valid leave type.");
        Guard.Ensure(startDate <= endDate, $"{nameof(startDate)} must be before or equal to {nameof(endDate)}.");

        var leave = new Leave(LeaveId.New())
        {
            EmployeeId = employeeId,
            Type = type,
            StartDate = startDate,
            EndDate = endDate,
            Reason = reason,
            Status = LeaveStatus.Pending,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        leave.RaiseDomainEvent(new LeaveRequestedDomainEvent(leave.Id, Guid.NewGuid(), Clock.UtcNow));

        return leave;
    }

    public void Approve(string approvedBy)
    {
        if (Status != LeaveStatus.Pending)
            return;

        Status = LeaveStatus.Approved;
        ApprovedBy = approvedBy;
        ApprovedAt = Clock.UtcNow;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new LeaveApprovedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Reject(string approvedBy)
    {
        if (Status != LeaveStatus.Pending)
            return;

        Status = LeaveStatus.Rejected;
        ApprovedBy = approvedBy;
        ApprovedAt = Clock.UtcNow;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new LeaveRejectedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Cancel()
    {
        if (Status == LeaveStatus.Cancelled)
            return;

        Status = LeaveStatus.Cancelled;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new LeaveCancelledDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum LeaveType
{
    Annual = 1,
    Sick = 2,
    Personal = 3,
    Maternity = 4,
    Paternity = 5,
    Unpaid = 6
}

public enum LeaveStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Cancelled = 4
}
