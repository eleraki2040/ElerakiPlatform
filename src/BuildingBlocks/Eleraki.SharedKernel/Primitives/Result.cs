namespace Eleraki.SharedKernel.Primitives;

/// <summary>
/// Represents the result of an operation that can succeed or fail.
/// </summary>
/// <typeparam name="TValue">The type of the success value.</typeparam>
public class Result<TValue>
{
    private readonly TValue? _value;
    private readonly Error? _error;

    /// <summary>
    /// Gets a value indicating whether the result is a success.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the result is a failure.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the success value.
    /// </summary>
    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("Result is not successful.");

    /// <summary>
    /// Gets the error.
    /// </summary>
    public Error Error => IsFailure ? _error! : throw new InvalidOperationException("Result is not a failure.");

    private Result(TValue value)
    {
        _value = value;
        IsSuccess = true;
    }

    private Result(Error error)
    {
        _error = error;
        IsSuccess = false;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <param name="value">The success value.</param>
    /// <returns>A successful Result.</returns>
    public static Result<TValue> Success(TValue value) => new(value);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>A failed Result.</returns>
    public static Result<TValue> Failure(Error error) => new(error);
}
