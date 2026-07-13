namespace Eleraki.SharedKernel.Primitives;

public static class Guard
{
    public static void NotNull(object? value, string parameterName)
    {
        if (value is null)
            throw new ArgumentNullException(parameterName);
    }

    public static void NotNullOrEmpty(string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{parameterName} cannot be null or empty.", parameterName);
    }

    public static void Ensure(bool condition, string message)
    {
        if (!condition)
            throw new ArgumentException(message);
    }
}
