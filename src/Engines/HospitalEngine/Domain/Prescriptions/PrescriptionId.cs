using Eleraki.HospitalEngine.Domain.Prescriptions;
using Eleraki.SharedKernel.Identity;

namespace Eleraki.HospitalEngine.Domain.Prescriptions;

public sealed record PrescriptionId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static PrescriptionId New() => new(Guid.NewGuid());
    public static PrescriptionId From(Guid value) => new(value);
}
