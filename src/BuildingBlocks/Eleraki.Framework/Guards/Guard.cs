namespace Eleraki.Framework.Guards;

/// <summary>
/// Provides guard clauses for validating method arguments.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the value is null.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public static void NotNull(object? value, string parameterName)
    {
        if (value is null)
            throw new ArgumentNullException(parameterName);
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the string is null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <exception cref="ArgumentException">Thrown when the string is null or empty.</exception>
    public static void NotNullOrEmpty(string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("String cannot be null or empty.", parameterName);
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the value is outside the specified range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum allowed value.</param>
    /// <param name="max">The maximum allowed value.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is outside the range.</exception>
    public static void InRange(int value, int min, int max, string parameterName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(parameterName, $"Value must be between {min} and {max}.");
    }
}