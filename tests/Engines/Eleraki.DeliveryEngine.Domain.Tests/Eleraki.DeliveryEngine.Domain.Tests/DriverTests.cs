using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.DeliveryEngine.Domain.Tests;

public class DriverTests
{
    [Fact]
    public void Create_Should_Return_Driver_With_Active_Status()
    {
        var driver = CreateValidDriver();

        driver.Status.Should().Be(DriverStatus.Active);
    }

    [Fact]
    public void Create_Should_Set_FullName()
    {
        var driver = CreateValidDriver();

        driver.FullName.Should().Be("Jane Smith");
    }

    [Fact]
    public void Create_Should_Set_LicenseNumber()
    {
        var driver = CreateValidDriver();

        driver.LicenseNumber.Should().Be("DL-789012");
    }

    [Fact]
    public void Create_Should_Set_Phone()
    {
        var driver = CreateValidDriver();

        driver.Phone.Value.Should().Be("+15551234567");
    }

    [Fact]
    public void Create_Should_Set_Email()
    {
        var driver = CreateValidDriver();

        driver.Email.Value.Should().Be("jane.smith@example.com");
    }

    [Fact]
    public void Create_Should_Assign_New_DriverId()
    {
        var driver = CreateValidDriver();

        driver.Id.Should().NotBe(default);
    }

    private static Driver CreateValidDriver()
    {
        var fullName = "Jane Smith";
        var licenseNumber = "DL-789012";
        var phone = PhoneNumber.Create("+15551234567");
        var email = Email.Create("jane.smith@example.com");

        return Driver.Create(fullName, licenseNumber, phone, email);
    }
}
