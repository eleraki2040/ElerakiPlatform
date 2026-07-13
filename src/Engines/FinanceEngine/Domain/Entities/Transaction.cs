using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.FinanceEngine.Domain;

public sealed class Transaction : AggregateRoot<TransactionId>
{
    public AccountId AccountId { get; private set; }
    public TransactionType Type { get; private set; }
    public Money Amount { get; private set; } = null!;
    public string? Description { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public TransactionStatus Status { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Transaction(TransactionId id) : base(id)
    {
        AccountId = default!;
        Amount = default!;
    }

    public static Transaction Create(AccountId accountId, TransactionType type, Money amount, string? description = null)
    {
        Guard.NotNull(accountId, nameof(accountId));
        Guard.NotNull(amount, nameof(amount));

        var transaction = new Transaction(TransactionId.New())
        {
            AccountId = accountId,
            Type = type,
            Amount = amount,
            Description = description,
            TransactionDate = Clock.UtcNow,
            Status = TransactionStatus.Draft,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        transaction.RaiseDomainEvent(new TransactionCreatedDomainEvent(transaction.Id, Guid.NewGuid(), Clock.UtcNow));

        return transaction;
    }

    public void Approve()
    {
        if (Status == TransactionStatus.Approved) return;
        Status = TransactionStatus.Approved;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new TransactionApprovedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Post()
    {
        if (Status == TransactionStatus.Posted) return;
        Status = TransactionStatus.Posted;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new TransactionPostedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Void()
    {
        Status = TransactionStatus.Voided;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new TransactionVoidedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum TransactionType
{
    Debit = 1,
    Credit = 2
}

public enum TransactionStatus
{
    Draft = 1,
    Approved = 2,
    Posted = 3,
    Voided = 4
}
