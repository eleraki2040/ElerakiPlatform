using Eleraki.SharedKernel.Primitives;

namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Contains error messages and codes for Party domain.
/// </summary>
public static class PartyErrors
{
    /// <summary>
    /// Error when party name is null or empty.
    /// </summary>
    public static readonly Error PartyNameRequired = Error.Custom(
        "Party.NameRequired",
        "Party name is required."
    );

    /// <summary>
    /// Error when party name exceeds maximum length.
    /// </summary>
    public static readonly Error PartyNameTooLong = Error.Custom(
        "Party.NameTooLong",
        $"Party name cannot exceed {PartyName.MaxLength} characters."
    );

    /// <summary>
    /// Error when party is not found.
    /// </summary>
    public static readonly Error PartyNotFound = Error.Custom(
        "Party.NotFound",
        "The specified party was not found."
    );
}
