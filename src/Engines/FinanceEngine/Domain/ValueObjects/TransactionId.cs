using Eleraki.SharedKernel.Identity;

namespace Eleraki.FinanceEngine.Domain;

public sealed record TransactionId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static TransactionId New() => new(Guid.NewGuid());
    public static TransactionId From(Guid value) => new(value);
}
