using Eleraki.OrganizationEngine.Domain.Events;
using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.OrganizationEngine.Domain.Organizations;

/// <summary>
/// Represents an organization in the system.
/// </summary>
public sealed class Organization : AggregateRoot<OrganizationId>
{
    /// <summary>
    /// Gets the organization name.
    /// </summary>
    public OrganizationName Name { get; private set; } = null!;

    /// <summary>
    /// Gets the organization code.
    /// </summary>
    public OrganizationCode Code { get; private set; } = null!;

    /// <summary>
    /// Gets the organization description.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the organization status.
    /// </summary>
    public OrganizationStatus Status { get; private set; }

    /// <summary>
    /// Gets the creation date and time.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the last update date and time.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    private Organization(OrganizationId id)
        : base(id)
    {
    }

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    /// <param name="name">The organization name.</param>
    /// <param name="code">The organization code.</param>
    /// <param name="description">Optional description.</param>
    /// <returns>A new Organization instance.</returns>
    public static Organization Create(string name, string code, string? description = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(code, nameof(code));

        var organization = new Organization(OrganizationId.New())
        {
            Name = OrganizationName.Create(name),
            Code = OrganizationCode.Create(code),
            Description = description,
            Status = OrganizationStatus.Active,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        organization.RaiseDomainEvent(new OrganizationCreatedDomainEvent(organization.Id, Guid.NewGuid(), Clock.UtcNow));

        return organization;
    }

    /// <summary>
    /// Updates the organization information.
    /// </summary>
    /// <param name="name">The new organization name.</param>
    /// <param name="description">Optional new description.</param>
    public void Update(string name, string? description = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        Name = OrganizationName.Create(name);
        Description = description;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    /// <summary>
    /// Activates the organization.
    /// </summary>
    public void Activate()
    {
        if (Status == OrganizationStatus.Active)
            return;

        Status = OrganizationStatus.Active;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    /// <summary>
    /// Deactivates the organization.
    /// </summary>
    public void Deactivate()
    {
        if (Status == OrganizationStatus.Inactive)
            return;

        Status = OrganizationStatus.Inactive;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

/// <summary>
/// Represents the status of an organization.
/// </summary>
public enum OrganizationStatus
{
    /// <summary>
    /// Organization is active.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Organization is inactive.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Organization is archived.
    /// </summary>
    Archived = 3
}