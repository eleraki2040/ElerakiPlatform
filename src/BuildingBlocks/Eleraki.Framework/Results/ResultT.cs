using Eleraki.Framework.Results;

namespace Eleraki.Framework.Results;

/// <summary>
/// Represents the result of an operation that returns a value.
/// </summary>
/// <typeparam name="TValue">The type of the value returned by the operation.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="value">The value returned by the operation.</param>
    /// <param name="error">The error that occurred, if any.</param>
    protected Result(bool isSuccess, TValue? value, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the value returned by the operation.
    /// </summary>
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failed result cannot be accessed.");

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <param name="value">The value returned by the operation.</param>
    public static Result<TValue> Success(TValue value) => new(true, value, Error.None);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The error that occurred.</param>
    public static new Result<TValue> Failure(Error error) => new(false, default, error);

    /// <summary>
    /// Implicitly converts a value to a successful result.
    /// </summary>
    public static implicit operator Result<TValue>(TValue? value) => value is not null ? Success(value) : Failure(Error.None);
}