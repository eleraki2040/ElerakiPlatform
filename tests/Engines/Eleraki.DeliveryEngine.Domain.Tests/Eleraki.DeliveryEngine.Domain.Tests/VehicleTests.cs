using Eleraki.DeliveryEngine.Domain.Vehicles;

namespace Eleraki.DeliveryEngine.Domain.Tests;

public class VehicleTests
{
    [Fact]
    public void Create_Should_Return_Vehicle_With_Available_Status()
    {
        var vehicle = Vehicle.Create("PD-123-XY", "Transit Van", 4);

        vehicle.Status.Should().Be(VehicleStatus.Available);
    }

    [Fact]
    public void Create_Should_Set_LicensePlate()
    {
        var vehicle = Vehicle.Create("PD-123-XY", "Transit Van", 4);

        vehicle.LicensePlate.Should().Be("PD-123-XY");
    }

    [Fact]
    public void Create_Should_Set_Model()
    {
        var vehicle = Vehicle.Create("PD-123-XY", "Transit Van", 4);

        vehicle.Model.Should().Be("Transit Van");
    }

    [Fact]
    public void Create_Should_Set_Capacity()
    {
        var vehicle = Vehicle.Create("PD-123-XY", "Transit Van", 4);

        vehicle.Capacity.Should().Be(4);
    }

    [Fact]
    public void Create_Should_Assign_New_VehicleId()
    {
        var vehicle = Vehicle.Create("PD-123-XY", "Transit Van", 4);

        vehicle.Id.Should().NotBe(default);
    }
}
