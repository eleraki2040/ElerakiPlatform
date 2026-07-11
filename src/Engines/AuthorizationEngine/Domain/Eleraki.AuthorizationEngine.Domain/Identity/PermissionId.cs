using Eleraki.SharedKernel.Identity;

namespace Eleraki.AuthorizationEngine.Domain;

public sealed record PermissionId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static PermissionId New() => new(Guid.NewGuid());
    public static PermissionId From(Guid value) => new(value);
}
