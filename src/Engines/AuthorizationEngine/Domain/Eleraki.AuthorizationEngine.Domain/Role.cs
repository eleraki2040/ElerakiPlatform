using Eleraki.AuthorizationEngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.AuthorizationEngine.Domain;

/// <summary>
/// Represents a role in the system.
/// </summary>
public sealed class Role : AggregateRoot<RoleId>
{
    /// <summary>
    /// Gets the role name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the role description.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets whether the role is active.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Gets the creation date and time.
    /// </summary>
    public DateTime CreatedOn { get; private set; }

    /// <summary>
    /// Gets the last modification date and time.
    /// </summary>
    public DateTime ModifiedOn { get; private set; }

    private Role(RoleId id)
        : base(id)
    {
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <param name="name">The role name.</param>
    /// <param name="description">The role description.</param>
    /// <returns>A new Role instance.</returns>
    public static Role Create(string name, string? description = null)
    {
        var role = new Role(RoleId.New())
        {
            Name = name,
            Description = description,
            IsActive = true,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        role.RaiseDomainEvent(new RoleCreatedDomainEvent(role.Id));

        return role;
    }

    /// <summary>
    /// Activates the role.
    /// </summary>
    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new RoleActivatedDomainEvent(Id));
    }

    /// <summary>
    /// Deactivates the role.
    /// </summary>
    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new RoleDeactivatedDomainEvent(Id));
    }

    /// <summary>
    /// Updates the role information.
    /// </summary>
    /// <param name="name">The new role name.</param>
    /// <param name="description">The new role description.</param>
    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new RoleUpdatedDomainEvent(Id));
    }
}
