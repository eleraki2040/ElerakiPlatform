using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.DeliveryEngine.Domain.Tests;

public class DeliveryTests
{
    [Fact]
    public void Create_Should_Return_Delivery_With_Pending_Status()
    {
        var trackingNumber = TrackingNumber.New();
        var delivery = Delivery.Create(trackingNumber, "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));

        delivery.Status.Should().Be(DeliveryStatus.Pending);
    }

    [Fact]
    public void Create_Should_Set_TrackingNumber()
    {
        var trackingNumber = TrackingNumber.From("TRK-ABC123");
        var delivery = Delivery.Create(trackingNumber, "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));

        delivery.TrackingNumber.Should().Be(trackingNumber);
    }

    [Fact]
    public void Create_Should_Set_RecipientName()
    {
        var delivery = Delivery.Create(TrackingNumber.New(), "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));

        delivery.RecipientName.Should().Be("John Doe");
    }

    [Fact]
    public void Create_Should_Set_DeliveryAddress()
    {
        var delivery = Delivery.Create(TrackingNumber.New(), "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));

        delivery.DeliveryAddress.Should().Be("123 Main St");
    }

    [Fact]
    public void Create_Should_Assign_New_DeliveryId()
    {
        var delivery = Delivery.Create(TrackingNumber.New(), "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));

        delivery.Id.Should().NotBe(default(Guid));
    }

    [Fact]
    public void AssignDriver_Should_Transition_To_Assigned()
    {
        var trackingNumber = TrackingNumber.New();
        var delivery = Delivery.Create(trackingNumber, "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));
        var driverId = DriverId.New();

        delivery.AssignDriver(driverId);

        delivery.Status.Should().Be(DeliveryStatus.Assigned);
        delivery.DriverId.Should().Be(driverId);
    }

    [Fact]
    public void AssignDriver_Should_Throw_When_Status_Is_Completed()
    {
        var delivery = CreateCompletedDelivery();
        var driverId = DriverId.New();

        delivery.Invoking(d => d.AssignDriver(driverId))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("*Cannot assign driver*");
    }

    [Fact]
    public void AssignDriver_Should_Throw_When_Status_Is_Cancelled()
    {
        var delivery = Delivery.Create(TrackingNumber.New(), "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));
        delivery.Cancel("Customer cancellation");

        var driverId = DriverId.New();

        delivery.Invoking(d => d.AssignDriver(driverId))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("*Cannot assign driver*");
    }

    [Fact]
    public void Start_Should_Transition_To_InTransit()
    {
        var trackingNumber = TrackingNumber.New();
        var delivery = Delivery.Create(trackingNumber, "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));
        delivery.AssignDriver(DriverId.New());

        delivery.Start();

        delivery.Status.Should().Be(DeliveryStatus.InTransit);
    }

    [Fact]
    public void Start_Should_Throw_When_Status_Is_Pending()
    {
        var delivery = Delivery.Create(TrackingNumber.New(), "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));

        delivery.Invoking(d => d.Start())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("*Cannot start delivery*");
    }

    [Fact]
    public void Complete_Should_Transition_To_Completed()
    {
        var delivery = CreateCompletedDelivery();

        delivery.Status.Should().Be(DeliveryStatus.Completed);
    }

    [Fact]
    public void Complete_Should_Set_DeliveredAt()
    {
        var delivery = CreateCompletedDelivery();

        delivery.DeliveredAt.Should().NotBeNull();
    }

    [Fact]
    public void Complete_Should_Throw_When_Status_Is_Not_InTransit()
    {
        var delivery = Delivery.Create(TrackingNumber.New(), "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));

        delivery.Invoking(d => d.Complete())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("*Cannot complete delivery*");
    }

    [Fact]
    public void Cancel_Should_Transition_To_Cancelled()
    {
        var delivery = Delivery.Create(TrackingNumber.New(), "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));
        var reason = "Customer requested cancellation";

        delivery.Cancel(reason);

        delivery.Status.Should().Be(DeliveryStatus.Cancelled);
        delivery.Notes.Should().Be(reason);
    }

    [Fact]
    public void Cancel_Should_Throw_When_Status_Is_Completed()
    {
        var delivery = CreateCompletedDelivery();

        delivery.Invoking(d => d.Cancel("reason"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("*Cannot cancel delivery*");
    }

    [Fact]
    public void Cancel_Should_Throw_When_Status_Is_Cancelled()
    {
        var delivery = Delivery.Create(TrackingNumber.New(), "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));
        delivery.Cancel("reason1");

        delivery.Invoking(d => d.Cancel("reason2"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("*Cannot cancel delivery*");
    }

    private static Delivery CreateCompletedDelivery()
    {
        var trackingNumber = TrackingNumber.New();
        var delivery = Delivery.Create(trackingNumber, "John Doe", "123 Main St", DateTime.UtcNow.AddDays(1));
        delivery.AssignDriver(DriverId.New());
        delivery.Start();
        delivery.Complete();
        return delivery;
    }
}
