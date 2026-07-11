namespace Eleraki.Framework.Primitives;

/// <summary>
/// Provides the current date and time.
/// </summary>
public static class Clock
{
    /// <summary>
    /// Gets the current UTC date and time.
    /// </summary>
    public static DateTime UtcNow => DateTime.UtcNow;

    /// <summary>
    /// Gets the current local date and time.
    /// </summary>
    public static DateTime Now => DateTime.Now;
}