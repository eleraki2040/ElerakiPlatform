using Eleraki.SharedKernel.Identity;

namespace Eleraki.HREngine.Domain;

public sealed record EmployeeId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static EmployeeId New() => new(Guid.NewGuid());
    public static EmployeeId From(Guid value) => new(value);
}

public sealed record DepartmentId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static DepartmentId New() => new(Guid.NewGuid());
    public static DepartmentId From(Guid value) => new(value);
}

public sealed record PositionId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static PositionId New() => new(Guid.NewGuid());
    public static PositionId From(Guid value) => new(value);
}

public sealed record SalaryId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static SalaryId New() => new(Guid.NewGuid());
    public static SalaryId From(Guid value) => new(value);
}

public sealed record AttendanceId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static AttendanceId New() => new(Guid.NewGuid());
    public static AttendanceId From(Guid value) => new(value);
}

public sealed record LeaveId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static LeaveId New() => new(Guid.NewGuid());
    public static LeaveId From(Guid value) => new(value);
}
