namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Represents the type of a Party.
/// </summary>
public enum PartyType
{
    /// <summary>
    /// Person (natural person).
    /// </summary>
    Person = 1,

    /// <summary>
    /// Organization (legal entity).
    /// </summary>
    Organization = 2,

    /// <summary>
    /// Customer role.
    /// </summary>
    Customer = 3,

    /// <summary>
    /// Supplier role.
    /// </summary>
    Supplier = 4,

    /// <summary>
    /// Employee role.
    /// </summary>
    Employee = 5,

    /// <summary>
    /// Bank role.
    /// </summary>
    Bank = 6,

    /// <summary>
    /// Government Agency role.
    /// </summary>
    GovernmentAgency = 7
}
