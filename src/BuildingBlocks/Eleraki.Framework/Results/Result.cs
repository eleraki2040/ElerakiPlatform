using Eleraki.Framework.Results;

namespace Eleraki.Framework.Results;

/// <summary>
/// Represents the result of an operation that does not return a value.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="error">The error that occurred, if any.</param>
    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the error that occurred.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The error that occurred.</param>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Implicitly converts a boolean to a success result.
    /// </summary>
    public static implicit operator Result(bool isSuccess) => isSuccess ? Success() : Failure(Error.Internal());
}