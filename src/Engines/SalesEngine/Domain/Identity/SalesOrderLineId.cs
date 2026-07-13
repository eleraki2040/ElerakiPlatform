using Eleraki.SharedKernel.Identity;

namespace Eleraki.SalesEngine.Domain.Identity;

public sealed record SalesOrderLineId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static SalesOrderLineId New() => new(Guid.NewGuid());
    public static SalesOrderLineId From(Guid value) => new(value);
}
