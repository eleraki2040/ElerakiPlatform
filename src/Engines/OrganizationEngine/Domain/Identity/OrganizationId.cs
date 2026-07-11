using Eleraki.SharedKernel.Identity;

namespace Eleraki.OrganizationEngine.Domain.Identity;

/// <summary>
/// Represents the unique identifier for an Organization.
/// </summary>
public sealed record OrganizationId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    /// <summary>
    /// Creates a new OrganizationId with a new GUID.
    /// </summary>
    public static OrganizationId New() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new OrganizationId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    public static OrganizationId From(Guid value) => new(value);
}