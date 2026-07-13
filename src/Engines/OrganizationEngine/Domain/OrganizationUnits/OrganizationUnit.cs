using Eleraki.OrganizationEngine.Domain.Events;
using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.OrganizationEngine.Domain.OrganizationUnits;

/// <summary>
/// Represents an OrganizationUnit in the organizational hierarchy.
/// </summary>
public sealed class OrganizationUnit : AggregateRoot<OrganizationUnitId>
{
    /// <summary>
    /// Gets the organization unit name.
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Gets the organization unit code.
    /// </summary>
    public string Code { get; private set; } = null!;

    /// <summary>
    /// Gets the organization unit description.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the organization ID this unit belongs to.
    /// </summary>
    public OrganizationId OrganizationId { get; private set; }

    /// <summary>
    /// Gets the parent organization unit ID.
    /// </summary>
    public OrganizationUnitId? ParentId { get; private set; }

    /// <summary>
    /// Gets the organization unit type ID.
    /// </summary>
    public OrganizationUnitTypeId OrganizationUnitTypeId { get; private set; }

    /// <summary>
    /// Gets the organization unit status.
    /// </summary>
    public OrganizationUnitStatus Status { get; private set; }

    /// <summary>
    /// Gets the creation date and time.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the last update date and time.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    private OrganizationUnit(OrganizationUnitId id)
        : base(id)
    {
        OrganizationId = default!;
        OrganizationUnitTypeId = default!;
    }

    /// <summary>
    /// Creates a new organization unit.
    /// </summary>
    /// <param name="organizationId">The owning organization ID.</param>
    /// <param name="name">The unit name.</param>
    /// <param name="code">The unit code.</param>
    /// <param name="organizationUnitTypeId">The unit type ID.</param>
    /// <param name="parentId">Optional parent unit ID.</param>
    /// <param name="description">Optional description.</param>
    /// <returns>A new OrganizationUnit instance.</returns>
    public static OrganizationUnit Create(
        OrganizationId organizationId,
        string name,
        string code,
        OrganizationUnitTypeId organizationUnitTypeId,
        OrganizationUnitId? parentId = null,
        string? description = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(code, nameof(code));

        var unit = new OrganizationUnit(OrganizationUnitId.New())
        {
            OrganizationId = organizationId,
            Name = name,
            Code = code,
            Description = description,
            OrganizationUnitTypeId = organizationUnitTypeId,
            ParentId = parentId,
            Status = OrganizationUnitStatus.Active,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        unit.RaiseDomainEvent(new OrganizationUnitCreatedDomainEvent(unit.Id, Guid.NewGuid(), Clock.UtcNow));

        return unit;
    }

    /// <summary>
    /// Updates the organization unit information.
    /// </summary>
    /// <param name="name">The new name.</param>
    /// <param name="code">The new code.</param>
    /// <param name="description">Optional new description.</param>
    public void Update(string name, string code, string? description = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(code, nameof(code));

        Name = name;
        Code = code;
        Description = description;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationUnitUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    /// <summary>
    /// Moves this organization unit under a new parent.
    /// </summary>
    /// <param name="newParentId">The new parent unit ID.</param>
    public void Move(OrganizationUnitId? newParentId)
    {
        ParentId = newParentId;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationUnitMovedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    /// <summary>
    /// Activates this organization unit.
    /// </summary>
    public void Activate()
    {
        if (Status == OrganizationUnitStatus.Active)
            return;

        Status = OrganizationUnitStatus.Active;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationUnitActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    /// <summary>
    /// Deactivates this organization unit.
    /// </summary>
    public void Deactivate()
    {
        if (Status == OrganizationUnitStatus.Inactive)
            return;

        Status = OrganizationUnitStatus.Inactive;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationUnitDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    /// <summary>
    /// Archives this organization unit.
    /// </summary>
    public void Archive()
    {
        if (Status == OrganizationUnitStatus.Archived)
            return;

        Status = OrganizationUnitStatus.Archived;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new OrganizationUnitArchivedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

/// <summary>
/// Represents the status of an organization unit.
/// </summary>
public enum OrganizationUnitStatus
{
    /// <summary>
    /// Organization unit is active.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Organization unit is inactive.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Organization unit is archived.
    /// </summary>
    Archived = 3
}
