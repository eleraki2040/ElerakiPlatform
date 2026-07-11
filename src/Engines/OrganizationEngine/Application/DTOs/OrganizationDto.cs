namespace Eleraki.OrganizationEngine.Application.DTOs;

/// <summary>
/// DTO for Organization.
/// </summary>
public class OrganizationDto
{
    /// <summary>
    /// Gets the organization identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the organization name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets the organization code.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets the organization description.
    /// </summary>
    public string? Description { get; set; }
}