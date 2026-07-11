namespace Eleraki.Framework.Results;

/// <summary>
/// Represents an error in the system.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Description">The error description.</param>
public record Error(string Code, string Description)
{
    /// <summary>
    /// Gets a value indicating whether the error is none.
    /// </summary>
    public bool IsNone => Code == Error.None.Code;

    /// <summary>
    /// Gets the none error.
    /// </summary>
    public static Error None { get; } = new(string.Empty, string.Empty);

    /// <summary>
    /// Gets the validation error.
    /// </summary>
    public static Error Validation(string description) => new("validation_error", description);

    /// <summary>
    /// Gets the not found error.
    /// </summary>
    public static Error NotFound(string description = "Resource not found.") => new("not_found", description);

    /// <summary>
    /// Gets the conflict error.
    /// </summary>
    public static Error Conflict(string description = "A conflict occurred.") => new("conflict", description);

    /// <summary>
    /// Gets the unauthorized error.
    /// </summary>
    public static Error Unauthorized(string description = "Unauthorized access.") => new("unauthorized", description);

    /// <summary>
    /// Gets the internal server error.
    /// </summary>
    public static Error Internal(string description = "An internal error occurred.") => new("internal_error", description);
}