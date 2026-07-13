using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.Identity;

namespace Eleraki.HospitalEngine.Domain.Admissions;

public sealed record AdmissionId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static AdmissionId New() => new(Guid.NewGuid());
    public static AdmissionId From(Guid value) => new(value);
}

public sealed record BedId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static BedId New() => new(Guid.NewGuid());
    public static BedId From(Guid value) => new(value);
}
