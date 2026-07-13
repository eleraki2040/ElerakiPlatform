using Eleraki.SharedKernel.Identity;

namespace Eleraki.FinanceEngine.Domain;

public sealed record AccountId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static AccountId New() => new(Guid.NewGuid());
    public static AccountId From(Guid value) => new(value);
}
