namespace Eleraki.IdentityEngine.Domain;

/// <summary>
/// Represents the role of a user.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// System administrator.
    /// </summary>
    Admin = 1,

    /// <summary>
    /// Regular user.
    /// </summary>
    User = 2,

    /// <summary>
    /// Manager.
    /// </summary>
    Manager = 3,

    /// <summary>
    /// Guest user.
    /// </summary>
    Guest = 4
}
