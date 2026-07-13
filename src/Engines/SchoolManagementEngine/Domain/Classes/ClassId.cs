using Eleraki.SharedKernel.Identity;

namespace Eleraki.SchoolManagementEngine.Domain.Classes;

public sealed record ClassId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static ClassId New() => new(Guid.NewGuid());
    public static ClassId From(Guid value) => new(value);
}
