namespace Eleraki.OrganizationEngine.Application.DTOs;

/// <summary>
/// Data transfer object for Organization.
/// </summary>
public class OrganizationDto
{
    /// <summary>
    /// Gets or sets the organization ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the organization name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the organization code.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the organization description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the organization status.
    /// </summary>
    public string Status { get; set; } = string.Empty;
}
