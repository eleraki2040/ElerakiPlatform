using Eleraki.SharedKernel.Primitives;

namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Domain errors for Enterprise.
/// </summary>
public static class EnterpriseErrors
{
    /// <summary>
    /// Enterprise not found.
    /// </summary>
    public static readonly Error NotFound = Error.NotFound("Enterprise not found.");

    /// <summary>
    /// Enterprise already exists.
    /// </summary>
    public static readonly Error AlreadyExists = Error.Conflict("Enterprise already exists.");

    /// <summary>
    /// Enterprise code already exists.
    /// </summary>
    public static readonly Error CodeAlreadyExists = Error.Conflict("Enterprise code already exists.");

    /// <summary>
    /// Cannot delete enterprise with active organization units.
    /// </summary>
    public static readonly Error CannotDeleteWithActiveUnits = Error.Validation("Cannot delete enterprise with active organization units.");

    /// <summary>
    /// Invalid enterprise status transition.
    /// </summary>
    public static readonly Error InvalidStatusTransition = Error.Validation("Invalid enterprise status transition.");
}