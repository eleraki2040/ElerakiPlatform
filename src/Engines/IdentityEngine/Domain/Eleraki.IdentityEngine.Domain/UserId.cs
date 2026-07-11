using Eleraki.SharedKernel.Identity;

namespace Eleraki.IdentityEngine.Domain;

/// <summary>
/// Represents the unique identifier for a User.
/// </summary>
public sealed record UserId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    /// <summary>
    /// Creates a new UserId with a new GUID.
    /// </summary>
    public static UserId New() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new UserId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    public static UserId From(Guid value) => new(value);
}
