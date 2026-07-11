using Eleraki.Framework.Results;

namespace Eleraki.Framework.Exceptions;

/// <summary>
/// Base exception for domain errors.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="error">The error that occurred.</param>
    public DomainException(Error error)
        : base(error.Description)
    {
        Error = error;
    }

    /// <summary>
    /// Gets the error associated with this exception.
    /// </summary>
    public Error Error { get; }
}