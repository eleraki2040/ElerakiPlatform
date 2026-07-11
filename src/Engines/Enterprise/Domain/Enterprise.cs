using Eleraki.Enterprise.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Represents an Enterprise (Organization) in the system.
/// </summary>
public sealed class Enterprise : AggregateRoot<EnterpriseId>
{
    /// <summary>
    /// Gets the enterprise code (Unique Business Identifier).
    /// </summary>
    public EnterpriseCode Code { get; private set; } = null!;

    /// <summary>
    /// Gets the enterprise name.
    /// </summary>
    public EnterpriseName Name { get; private set; } = null!;

    /// <summary>
    /// Gets the legal name.
    /// </summary>
    public string? LegalName { get; private set; }

    /// <summary>
    /// Gets the enterprise status.
    /// </summary>
    public EnterpriseStatus Status { get; private set; }

    /// <summary>
    /// Gets the creation date and time.
    /// </summary>
    public DateTime CreatedOn { get; private set; }

    /// <summary>
    /// Gets the last modification date and time.
    /// </summary>
    public DateTime ModifiedOn { get; private set; }

    private Enterprise(EnterpriseId id)
        : base(id)
    {
    }

    /// <summary>
    /// Creates a new Enterprise.
    /// </summary>
    /// <param name="code">The enterprise code.</param>
    /// <param name="name">The enterprise name.</param>
    /// <param name="legalName">Optional legal name.</param>
    /// <returns>A new Enterprise instance.</returns>
    public static Enterprise Create(EnterpriseCode code, EnterpriseName name, string? legalName = null)
    {
        Guard.NotNullOrEmpty(code.Value, nameof(code));
        Guard.NotNullOrEmpty(name.Value, nameof(name));

        var enterprise = new Enterprise(EnterpriseId.New())
        {
            Code = code,
            Name = name,
            LegalName = legalName,
            Status = EnterpriseStatus.Active,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        enterprise.RaiseDomainEvent(new EnterpriseCreatedDomainEvent(enterprise.Id));

        return enterprise;
    }

    /// <summary>
    /// Updates the enterprise information.
    /// </summary>
    /// <param name="name">The new enterprise name.</param>
    /// <param name="legalName">Optional legal name.</param>
    public void Update(EnterpriseName name, string? legalName = null)
    {
        Guard.NotNullOrEmpty(name.Value, nameof(name));

        Name = name;
        LegalName = legalName;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new EnterpriseUpdatedDomainEvent(Id));
    }

    /// <summary>
    /// Activates the enterprise.
    /// </summary>
    public void Activate()
    {
        if (Status == EnterpriseStatus.Active)
            return;

        Status = EnterpriseStatus.Active;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new EnterpriseActivatedDomainEvent(Id));
    }

    /// <summary>
    /// Deactivates the enterprise.
    /// </summary>
    public void Deactivate()
    {
        if (Status == EnterpriseStatus.Inactive)
            return;

        Status = EnterpriseStatus.Inactive;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new EnterpriseDeactivatedDomainEvent(Id));
    }

    /// <summary>
    /// Suspends the enterprise.
    /// </summary>
    public void Suspend()
    {
        if (Status == EnterpriseStatus.Suspended)
            return;

        Status = EnterpriseStatus.Suspended;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new EnterpriseSuspendedDomainEvent(Id));
    }

    /// <summary>
    /// Archives the enterprise.
    /// </summary>
    public void Archive()
    {
        if (Status == EnterpriseStatus.Archived)
            return;

        Status = EnterpriseStatus.Archived;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new EnterpriseArchivedDomainEvent(Id));
    }
}

/// <summary>
/// Represents the status of an Enterprise.
/// </summary>
public enum EnterpriseStatus
{
    /// <summary>
    /// Enterprise is active.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Enterprise is inactive.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Enterprise is suspended.
    /// </summary>
    Suspended = 3,

    /// <summary>
    /// Enterprise is archived.
    /// </summary>
    Archived = 4
}
