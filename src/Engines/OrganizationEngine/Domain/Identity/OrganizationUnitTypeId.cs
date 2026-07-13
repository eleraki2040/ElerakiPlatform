using Eleraki.SharedKernel.Identity;

namespace Eleraki.OrganizationEngine.Domain.Identity;

/// <summary>
/// Represents the unique identifier for an OrganizationUnitType.
/// </summary>
public sealed record OrganizationUnitTypeId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    /// <summary>
    /// Creates a new OrganizationUnitTypeId with a new GUID.
    /// </summary>
    public static OrganizationUnitTypeId New() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new OrganizationUnitTypeId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    public static OrganizationUnitTypeId From(Guid value) => new(value);
}
