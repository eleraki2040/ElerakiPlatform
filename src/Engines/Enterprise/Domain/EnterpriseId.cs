using Eleraki.SharedKernel.Identity;

namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Represents the unique identifier for an Enterprise.
/// </summary>
public sealed record EnterpriseId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    /// <summary>
    /// Creates a new EnterpriseId with a new GUID.
    /// </summary>
    public static EnterpriseId New() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new EnterpriseId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    public static EnterpriseId From(Guid value) => new(value);
}