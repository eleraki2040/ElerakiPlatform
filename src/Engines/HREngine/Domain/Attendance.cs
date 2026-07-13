using Eleraki.HREngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.HREngine.Domain;

public sealed class Attendance : AggregateRoot<AttendanceId>
{
    public string EmployeeId { get; private set; } = string.Empty;
    public DateTime AttendanceDate { get; private set; }
    public DateTime? CheckInTime { get; private set; }
    public DateTime? CheckOutTime { get; private set; }
    public AttendanceStatus Status { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Attendance(AttendanceId id) : base(id)
    {
    }

    public static Attendance Record(string employeeId, DateTime attendanceDate, DateTime? checkInTime = null, string? notes = null)
    {
        Guard.NotNullOrEmpty(employeeId, nameof(employeeId));

        var attendance = new Attendance(AttendanceId.New())
        {
            EmployeeId = employeeId,
            AttendanceDate = attendanceDate.Date,
            CheckInTime = checkInTime,
            Status = AttendanceStatus.Present,
            Notes = notes,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        attendance.RaiseDomainEvent(new AttendanceRecordedDomainEvent(attendance.Id, Guid.NewGuid(), Clock.UtcNow));

        return attendance;
    }

    public void CheckOut(DateTime checkOutTime)
    {
        CheckOutTime = checkOutTime;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new AttendanceCheckedOutDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void MarkAbsent(string? notes = null)
    {
        Status = AttendanceStatus.Absent;
        Notes = notes;
        UpdatedAt = Clock.UtcNow;
    }
}

public enum AttendanceStatus
{
    Present = 1,
    Absent = 2,
    Late = 3,
    OnLeave = 4
}
