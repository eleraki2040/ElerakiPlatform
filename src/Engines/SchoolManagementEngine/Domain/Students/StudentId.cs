using Eleraki.SharedKernel.Identity;

namespace Eleraki.SchoolManagementEngine.Domain.Students;

public sealed record StudentId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static StudentId New() => new(Guid.NewGuid());
    public static StudentId From(Guid value) => new(value);
}
