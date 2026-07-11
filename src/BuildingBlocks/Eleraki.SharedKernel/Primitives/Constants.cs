namespace Eleraki.SharedKernel.Primitives;

/// <summary>
/// Contains platform-wide constants.
/// </summary>
public static class Constants
{
    /// <summary>
    /// The maximum allowed hierarchy depth.
    /// </summary>
    public const int MaxHierarchyDepth = 10;

    /// <summary>
    /// The default page size for pagination.
    /// </summary>
    public const int DefaultPageSize = 20;

    /// <summary>
    /// The maximum page size for pagination.
    /// </summary>
    public const int MaxPageSize = 100;

    /// <summary>
    /// The maximum length for organization name.
    /// </summary>
    public const int MaxNameLength = 200;

    /// <summary>
    /// The maximum length for organization code.
    /// </summary>
    public const int MaxCodeLength = 50;
}