using Eleraki.SharedKernel.Identity;

namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Represents the unique identifier for a Party.
/// </summary>
public sealed record PartyId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    /// <summary>
    /// Creates a new PartyId with a new GUID.
    /// </summary>
    public static PartyId New() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new PartyId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    public static PartyId From(Guid value) => new(value);
}
