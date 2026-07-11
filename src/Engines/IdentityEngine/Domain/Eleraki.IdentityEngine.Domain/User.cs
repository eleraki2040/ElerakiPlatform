using Eleraki.IdentityEngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.IdentityEngine.Domain;

/// <summary>
/// Represents a user in the system.
/// </summary>
public sealed class User : AggregateRoot<UserId>
{
    /// <summary>
    /// Gets the user name.
    /// </summary>
    public UserName Name { get; private set; } = null!;

    /// <summary>
    /// Gets the user email.
    /// </summary>
    public Email Email { get; private set; } = null!;

    /// <summary>
    /// Gets the user password.
    /// </summary>
    public UserPassword Password { get; private set; } = null!;

    /// <summary>
    /// Gets the user role.
    /// </summary>
    public UserRole Role { get; private set; }

    /// <summary>
    /// Gets whether the user is active.
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

    private User(UserId id)
        : base(id)
    {
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="name">The user name.</param>
    /// <param name="email">The user email.</param>
    /// <param name="password">The user password.</param>
    /// <param name="role">The user role.</param>
    /// <returns>A new User instance.</returns>
    public static User Create(UserName name, Email email, UserPassword password, UserRole role = UserRole.User)
    {
        var user = new User(UserId.New())
        {
            Name = name,
            Email = email,
            Password = password,
            Role = role,
            IsActive = true,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    /// <summary>
    /// Activates the user.
    /// </summary>
    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new UserActivatedDomainEvent(Id));
    }

    /// <summary>
    /// Deactivates the user.
    /// </summary>
    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new UserDeactivatedDomainEvent(Id));
    }

    /// <summary>
    /// Changes the user role.
    /// </summary>
    /// <param name="newRole">The new role.</param>
    public void ChangeRole(UserRole newRole)
    {
        if (Role == newRole)
            return;

        Role = newRole;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new UserRoleChangedDomainEvent(Id, Role));
    }
}
