using Eleraki.SharedKernel.Identity;

namespace Eleraki.SchoolManagementEngine.Domain.Courses;

public sealed record CourseId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static CourseId New() => new(Guid.NewGuid());
    public static CourseId From(Guid value) => new(value);
}
