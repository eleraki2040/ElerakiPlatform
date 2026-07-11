namespace Eleraki.SharedKernel.Primitives;

/// <summary>
/// Represents an error with a code and message.
/// </summary>
public sealed class Error
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string Message { get; }

    private Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Creates a new error.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>A new Error instance.</returns>
    public static Error Custom(string code, string message) => new(code, message);

    /// <summary>
    /// Creates a not found error.
    /// </summary>
    /// <param name="message">The error message.</param>
    public static Error NotFound(string message) => new("NotFound", message);

    /// <summary>
    /// Creates a conflict error.
    /// </summary>
    /// <param name="message">The error message.</param>
    public static Error Conflict(string message) => new("Conflict", message);

    /// <summary>
    /// Creates a validation error.
    /// </summary>
    /// <param name="message">The error message.</param>
    public static Error Validation(string message) => new("Validation", message);
}
