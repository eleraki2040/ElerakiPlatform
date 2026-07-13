using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.FinanceEngine.Domain;

public sealed class JournalEntryLine
{
    public Guid Id { get; private set; }
    public JournalEntryId JournalEntryId { get; private set; } = default!;
    public AccountId AccountId { get; private set; } = default!;
    public string Description { get; private set; } = string.Empty;
    public Money Amount { get; private set; } = null!;
    public JournalEntryLineType Type { get; private set; }
    public int LineNumber { get; private set; }

    private JournalEntryLine()
    {
    }

    public static JournalEntryLine Create(int lineNumber, AccountId accountId, Money amount, string description, JournalEntryLineType type)
    {
        Guard.NotNull(accountId, nameof(accountId));
        Guard.NotNull(amount, nameof(amount));
        Guard.NotNullOrEmpty(description, nameof(description));

        var line = new JournalEntryLine
        {
            Id = Guid.NewGuid(),
            LineNumber = lineNumber,
            AccountId = accountId,
            Amount = amount,
            Description = description,
            Type = type
        };

        return line;
    }
}

public enum JournalEntryLineType
{
    Debit = 1,
    Credit = 2
}
