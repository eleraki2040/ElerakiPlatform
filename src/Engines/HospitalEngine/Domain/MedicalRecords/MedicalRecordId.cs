using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.Identity;

namespace Eleraki.HospitalEngine.Domain.MedicalRecords;

public sealed record MedicalRecordId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static MedicalRecordId New() => new(Guid.NewGuid());
    public static MedicalRecordId From(Guid value) => new(value);
}

public sealed record DoctorId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static DoctorId New() => new(Guid.NewGuid());
    public static DoctorId From(Guid value) => new(value);
}
