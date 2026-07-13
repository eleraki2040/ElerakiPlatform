using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Admissions;
using Eleraki.HospitalEngine.Domain.Billing;
using Eleraki.SharedKernel.ValueObjects;
using Xunit;

namespace Eleraki.HospitalEngine.Domain.Tests;

public class PatientTests
{
    [Fact]
    public void Create_Should_Return_Patient_With_Active_Status()
    {
        var name = PersonName.Create("John Doe");
        var email = Email.Create("john@example.com");
        var phone = PhoneNumber.Create("+201234567890");
        var dob = new DateTime(1990, 1, 1);

        var patient = Patient.Create(name, email, phone, dob);

        Assert.NotNull(patient);
        Assert.Equal(name.Value, patient.Name.Value);
        Assert.Equal(email.Value, patient.Email.Value);
        Assert.Equal(PatientStatus.Active, patient.Status);
        Assert.NotEqual(default, patient.Id);
    }

    [Fact]
    public void Create_Should_Raise_PatientRegisteredDomainEvent()
    {
        var name = PersonName.Create("John Doe");
        var email = Email.Create("john@example.com");
        var phone = PhoneNumber.Create("+201234567890");
        var dob = new DateTime(1990, 1, 1);

        var patient = Patient.Create(name, email, phone, dob);

        Assert.Contains(patient.DomainEvents, e => e.GetType().Name == "PatientRegisteredDomainEvent");
    }

    [Fact]
    public void Deactivate_Should_Set_Status_To_Inactive()
    {
        var name = PersonName.Create("John Doe");
        var email = Email.Create("john@example.com");
        var phone = PhoneNumber.Create("+201234567890");
        var dob = new DateTime(1990, 1, 1);
        var patient = Patient.Create(name, email, phone, dob);

        patient.Deactivate();

        Assert.Equal(PatientStatus.Inactive, patient.Status);
    }
}
