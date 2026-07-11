using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.IdentityEngine.Domain;

/// <summary>
/// Represents a user password.
/// </summary>
public sealed class UserPassword : ValueObject
{
    /// <summary>
    /// Gets the maximum length for user password.
    /// </summary>
    public const int MaxLength = 256;

    /// <summary>
    /// Gets the hashed password value.
    /// </summary>
    public string Value { get; }

    private UserPassword(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new user password from plain text.
    /// </summary>
    /// <param name="plainTextPassword">The plain text password.</param>
    /// <returns>A new UserPassword instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the password is invalid.</exception>
    public static UserPassword Create(string plainTextPassword)
    {
        Guard.NotNullOrEmpty(plainTextPassword, nameof(plainTextPassword));

        if (plainTextPassword.Length < 6)
            throw new ArgumentException("Password must be at least 6 characters.");

        if (plainTextPassword.Length > MaxLength)
            throw new ArgumentException($"Password cannot exceed {MaxLength} characters.");

        return new UserPassword(plainTextPassword);
    }

    /// <summary>
    /// Creates a UserPassword from an existing hash.
    /// </summary>
    /// <param name="hash">The hashed password.</param>
    /// <returns>A new UserPassword instance.</returns>
    public static UserPassword FromHash(string hash)
    {
        Guard.NotNullOrEmpty(hash, nameof(hash));
        return new UserPassword(hash);
    }

    /// <inheritdoc/>
    protected override bool EqualsCore(IValueObject other)
    {
        if (other is not UserPassword otherPassword) return false;
        return Value == otherPassword.Value;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <inheritdoc/>
    public override string ToString() => "******";
}
