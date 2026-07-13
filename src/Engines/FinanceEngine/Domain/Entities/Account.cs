using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.FinanceEngine.Domain;

public sealed class Account : AggregateRoot<AccountId>
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public AccountType Type { get; private set; }
    public string? ParentAccountId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Account(AccountId id) : base(id)
    {
    }

    public static Account Create(string name, string code, AccountType type, string? parentAccountId = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(code, nameof(code));

        var account = new Account(AccountId.New())
        {
            Name = name,
            Code = code.Trim().ToUpperInvariant(),
            Type = type,
            ParentAccountId = parentAccountId,
            IsActive = true,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        account.RaiseDomainEvent(new AccountCreatedDomainEvent(account.Id, Guid.NewGuid(), Clock.UtcNow));

        return account;
    }

    public void Update(string name, string? parentAccountId = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        Name = name;
        ParentAccountId = parentAccountId;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new AccountUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Activate()
    {
        if (IsActive) return;
        IsActive = true;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new AccountActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
        ModifiedOn = Clock.UtcNow;
        RaiseDomainEvent(new AccountDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum AccountType
{
    Asset = 1,
    Liability = 2,
    Equity = 3,
    Revenue = 4,
    Expense = 5
}
