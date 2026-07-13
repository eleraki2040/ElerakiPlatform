using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Patients;
using Xunit;

namespace Eleraki.HospitalEngine.Domain.Tests;

public class AppointmentTests
{
    [Fact]
    public void Create_Should_Return_Appointment_With_Scheduled_Status()
    {
        var patientId = PatientId.New();
        var doctorId = DoctorId.New();
        var scheduledAt = DateTime.UtcNow.AddDays(1);

        var appointment = Appointment.Create(patientId, doctorId, scheduledAt);

        Assert.NotNull(appointment);
        Assert.Equal(patientId, appointment.PatientId);
        Assert.Equal(doctorId, appointment.DoctorId);
        Assert.Equal(AppointmentStatus.Scheduled, appointment.Status);
        Assert.NotEqual(default, appointment.Id);
    }

    [Fact]
    public void Complete_Should_Set_Status_To_Completed()
    {
        var patientId = PatientId.New();
        var doctorId = DoctorId.New();
        var scheduledAt = DateTime.UtcNow.AddDays(-1);
        var appointment = Appointment.Create(patientId, doctorId, scheduledAt);

        appointment.Complete();

        Assert.Equal(AppointmentStatus.Completed, appointment.Status);
    }

    [Fact]
    public void Cancel_Should_Set_Status_To_Cancelled()
    {
        var patientId = PatientId.New();
        var doctorId = DoctorId.New();
        var scheduledAt = DateTime.UtcNow.AddDays(1);
        var appointment = Appointment.Create(patientId, doctorId, scheduledAt);

        appointment.Cancel();

        Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
    }
}
