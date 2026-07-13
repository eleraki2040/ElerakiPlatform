using Eleraki.SharedKernel.Identity;

namespace Eleraki.FinanceEngine.Domain;

public sealed record JournalEntryId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static JournalEntryId New() => new(Guid.NewGuid());
    public static JournalEntryId From(Guid value) => new(value);
}
