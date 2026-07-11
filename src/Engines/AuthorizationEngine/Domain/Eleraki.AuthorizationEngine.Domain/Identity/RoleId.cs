using Eleraki.SharedKernel.Identity;

namespace Eleraki.AuthorizationEngine.Domain;

public sealed record RoleId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static RoleId New() => new(Guid.NewGuid());
    public static RoleId From(Guid value) => new(value);
}
