using Eleraki.SharedKernel.Identity;

namespace Eleraki.SchoolManagementEngine.Domain.Teachers;

public sealed record TeacherId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static TeacherId New() => new(Guid.NewGuid());
    public static TeacherId From(Guid value) => new(value);
}
