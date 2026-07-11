using Eleraki.Framework.Results;

namespace Eleraki.Framework.Exceptions;

/// <summary>
/// Exception thrown when a business rule is violated.
/// </summary>
public class BusinessRuleException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleException"/> class.
    /// </summary>
    /// <param name="error">The error that occurred.</param>
    public BusinessRuleException(Error error)
        : base(error)
    {
    }
}