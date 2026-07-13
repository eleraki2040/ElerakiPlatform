using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.Events;

namespace Eleraki.FinanceEngine.Domain;

public sealed record AccountCreatedDomainEvent(AccountId AccountId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record AccountUpdatedDomainEvent(AccountId AccountId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record AccountActivatedDomainEvent(AccountId AccountId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record AccountDeactivatedDomainEvent(AccountId AccountId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record TransactionCreatedDomainEvent(TransactionId TransactionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record TransactionApprovedDomainEvent(TransactionId TransactionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record TransactionPostedDomainEvent(TransactionId TransactionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record TransactionVoidedDomainEvent(TransactionId TransactionId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record JournalEntryCreatedDomainEvent(JournalEntryId JournalEntryId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
public sealed record JournalEntryPostedDomainEvent(JournalEntryId JournalEntryId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
