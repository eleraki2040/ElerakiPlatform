using Eleraki.SharedKernel.Identity;

namespace Eleraki.SalesEngine.Domain.Identity;

public sealed record SalesOrderId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static SalesOrderId New() => new(Guid.NewGuid());
    public static SalesOrderId From(Guid value) => new(value);
}
