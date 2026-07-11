using Eleraki.SharedKernel.Primitives;

namespace Eleraki.AuthorizationEngine.Domain;

public static class AuthorizationErrors
{
    public static readonly Error PermissionNotFound = Error.NotFound("Permission not found.");
    public static readonly Error RoleNotFound = Error.NotFound("Role not found.");
    public static readonly Error PermissionAlreadyExists = Error.Conflict("Permission already exists.");
    public static readonly Error RoleAlreadyExists = Error.Conflict("Role already exists.");
}
