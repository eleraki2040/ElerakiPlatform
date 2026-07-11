using Eleraki.Enterprise.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.Enterprise.Domain;

/// <summary>
/// Represents a Party (any person or organization that interacts with the enterprise).
/// </summary>
public sealed class Party : AggregateRoot<PartyId>
{
    /// <summary>
    /// Gets the party name.
    /// </summary>
    public PartyName Name { get; private set; } = null!;

    /// <summary>
    /// Gets the party type.
    /// </summary>
    public PartyType Type { get; private set; }

    /// <summary>
    /// Gets the party status.
    /// </summary>
    public PartyStatus Status { get; private set; }

    /// <summary>
    /// Gets the creation date and time.
    /// </summary>
    public DateTime CreatedOn { get; private set; }

    /// <summary>
    /// Gets the last modification date and time.
    /// </summary>
    public DateTime ModifiedOn { get; private set; }

    private Party(PartyId id)
        : base(id)
    {
    }

    /// <summary>
    /// Creates a new Party.
    /// </summary>
    /// <param name="name">The party name.</param>
    /// <param name="type">The party type.</param>
    /// <returns>A new Party instance.</returns>
    public static Party Create(PartyName name, PartyType type)
    {
        Guard.NotNullOrEmpty(name.Value, nameof(name));

        var party = new Party(PartyId.New())
        {
            Name = name,
            Type = type,
            Status = PartyStatus.Active,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        party.RaiseDomainEvent(new PartyCreatedDomainEvent(party.Id));

        return party;
    }

    /// <summary>
    /// Updates the party information.
    /// </summary>
    /// <param name="name">The new party name.</param>
    public void Update(PartyName name)
    {
        Guard.NotNullOrEmpty(name.Value, nameof(name));

        Name = name;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PartyUpdatedDomainEvent(Id));
    }

    /// <summary>
    /// Activates the party.
    /// </summary>
    public void Activate()
    {
        if (Status == PartyStatus.Active)
            return;

        Status = PartyStatus.Active;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PartyActivatedDomainEvent(Id));
    }

    /// <summary>
    /// Deactivates the party.
    /// </summary>
    public void Deactivate()
    {
        if (Status == PartyStatus.Inactive)
            return;

        Status = PartyStatus.Inactive;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PartyDeactivatedDomainEvent(Id));
    }

    /// <summary>
    /// Archives the party.
    /// </summary>
    public void Archive()
    {
        if (Status == PartyStatus.Archived)
            return;

        Status = PartyStatus.Archived;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PartyArchivedDomainEvent(Id));
    }
}

/// <summary>
/// Represents the status of a Party.
/// </summary>
public enum PartyStatus
{
    /// <summary>
    /// Party is active.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Party is inactive.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Party is archived.
    /// </summary>
    Archived = 3
}
