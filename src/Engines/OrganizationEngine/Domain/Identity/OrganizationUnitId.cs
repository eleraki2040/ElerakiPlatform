using Eleraki.SharedKernel.Identity;

namespace Eleraki.OrganizationEngine.Domain.Identity;

/// <summary>
/// Represents the unique identifier for an OrganizationUnit.
/// </summary>
public sealed record OrganizationUnitId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    /// <summary>
    /// Creates a new OrganizationUnitId with a new GUID.
    /// </summary>
    public static OrganizationUnitId New() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new OrganizationUnitId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    public static OrganizationUnitId From(Guid value) => new(value);
}
