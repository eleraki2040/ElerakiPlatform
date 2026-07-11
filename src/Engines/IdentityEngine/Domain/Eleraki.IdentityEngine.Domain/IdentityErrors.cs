using Eleraki.SharedKernel.Primitives;

namespace Eleraki.IdentityEngine.Domain;

/// <summary>
/// Domain errors for Identity Engine.
/// </summary>
public static class IdentityErrors
{
    /// <summary>
    /// User not found.
    /// </summary>
    public static readonly Error UserNotFound = Error.NotFound("User not found.");

    /// <summary>
    /// Email already exists.
    /// </summary>
    public static readonly Error EmailAlreadyExists = Error.Conflict("Email already exists.");

    /// <summary>
    /// Invalid password.
    /// </summary>
    public static readonly Error InvalidPassword = Error.Validation("Invalid password.");
}
