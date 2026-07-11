using Eleraki.Framework.Results;

namespace Eleraki.Framework.Exceptions;

/// <summary>
/// Exception thrown when validation fails.
/// </summary>
public class ValidationException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    /// <param name="validationErrors">The collection of validation errors.</param>
    public ValidationException(IEnumerable<Error> validationErrors)
        : base(Error.Validation("One or more validation errors occurred."))
    {
        ValidationErrors = validationErrors.ToList();
    }

    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    public List<Error> ValidationErrors { get; }
}