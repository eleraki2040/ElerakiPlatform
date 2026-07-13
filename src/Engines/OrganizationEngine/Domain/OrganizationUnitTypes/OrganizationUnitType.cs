using Eleraki.OrganizationEngine.Domain.Events;
using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.OrganizationEngine.Domain.OrganizationUnitTypes;

/// <summary>
/// Represents an OrganizationUnitType aggregate root.
/// </summary>
public sealed class OrganizationUnitType : AggregateRoot<OrganizationUnitTypeId>
{
    /// <summary>
    /// Gets the organization unit type name.
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Gets the organization unit type description.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets whether this type is active.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Gets the creation date and time.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the last update date and time.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    private OrganizationUnitType(OrganizationUnitTypeId id)
        : base(id)
    {
    }

    /// <summary>
    /// Creates a new organization unit type.
    /// </summary>
    /// <param name="name">The type name.</param>
    /// <param name="description">Optional description.</param>
    /// <returns>A new OrganizationUnitType instance.</returns>
    public static OrganizationUnitType Create(string name, string? description = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        var type = new OrganizationUnitType(OrganizationUnitTypeId.New())
        {
            Name = name,
            Description = description,
            IsActive = true,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        type.RaiseDomainEvent(new OrganizationUnitTypeCreatedDomainEvent(type.Id, Guid.NewGuid(), Clock.UtcNow));

        return type;
    }

    /// <summary>
    /// Updates the organization unit type information.
    /// </summary>
    /// <param name="name">The new name.</param>
    /// <param name="description">Optional new description.</param>
    public void Update(string name, string? description = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        Name = name;
        Description = description;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationUnitTypeUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    /// <summary>
    /// Activates this organization unit type.
    /// </summary>
    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationUnitTypeActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    /// <summary>
    /// Deactivates this organization unit type.
    /// </summary>
    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationUnitTypeDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}
