using Eleraki.SharedKernel.Identity;

namespace Eleraki.HospitalEngine.Domain.Patients;

public sealed record PatientId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static PatientId New() => new(Guid.NewGuid());
    public static PatientId From(Guid value) => new(value);
}
