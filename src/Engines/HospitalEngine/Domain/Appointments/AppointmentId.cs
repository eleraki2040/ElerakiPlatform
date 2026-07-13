using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.Identity;

namespace Eleraki.HospitalEngine.Domain.Appointments;

public sealed record AppointmentId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static AppointmentId New() => new(Guid.NewGuid());
    public static AppointmentId From(Guid value) => new(value);
}

public sealed record DoctorId(Guid Value) : StronglyTypedId<Guid>(Value)
{
    public static DoctorId New() => new(Guid.NewGuid());
    public static DoctorId From(Guid value) => new(value);
}
