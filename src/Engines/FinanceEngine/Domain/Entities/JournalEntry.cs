using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.FinanceEngine.Domain;

public sealed class JournalEntry : AggregateRoot<JournalEntryId>
{
    public string ReferenceNumber { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public JournalEntryStatus Status { get; private set; }
    public DateTime EntryDate { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private JournalEntry(JournalEntryId id) : base(id)
    {
    }

    public static JournalEntry Create(string description)
    {
        Guard.NotNullOrEmpty(description, nameof(description));

        var entry = new JournalEntry(JournalEntryId.New())
        {
            ReferenceNumber = GenerateReferenceNumber(),
            Description = description,
            Status = JournalEntryStatus.Draft,
            EntryDate = Clock.UtcNow,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        entry.RaiseDomainEvent(new JournalEntryCreatedDomainEvent(entry.Id, Guid.NewGuid(), Clock.UtcNow));

        return entry;
    }

    public void Post()
    {
        Status = JournalEntryStatus.Posted;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new JournalEntryPostedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    private static string GenerateReferenceNumber()
    {
        return $"JE-{Clock.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";
    }
}

public enum JournalEntryStatus
{
    Draft = 1,
    Posted = 2,
    Cancelled = 3
}
