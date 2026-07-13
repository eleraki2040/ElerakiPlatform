using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.Identity;

namespace Eleraki.HospitalEngine.Domain.Billing;

public sealed record InvoiceId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static InvoiceId New() => new(Guid.NewGuid());
    public static InvoiceId From(Guid value) => new(value);
}
