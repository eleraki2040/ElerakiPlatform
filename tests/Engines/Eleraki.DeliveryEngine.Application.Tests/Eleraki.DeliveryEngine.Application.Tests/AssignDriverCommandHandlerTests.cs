using Eleraki.DeliveryEngine.Application.Commands;
using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Drivers;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;
using MediatR;
using Moq;
using Xunit;

namespace Eleraki.DeliveryEngine.Application.Tests;

public class AssignDriverCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_DeliveryAndDriverExist()
    {
        var deliveryRepositoryMock = new Mock<IDeliveryRepository>();
        var driverRepositoryMock = new Mock<IDriverRepository>();

        var deliveryId = Guid.NewGuid();
        var driverId = DriverId.New();

        var delivery = Delivery.Create(
            TrackingNumber.New(),
            "John Doe",
            "123 Main St",
            DateTime.UtcNow.AddDays(1));

        deliveryRepositoryMock.Setup(r => r.GetByIdAsync(deliveryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(delivery);

        driverRepositoryMock.Setup(r => r.GetByIdAsync(driverId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Driver.Create(
                "Jane Smith",
                "DL-789",
                PhoneNumber.Create("+15551234567"),
                Email.Create("jane@example.com")));

        var handler = new AssignDriverCommandHandler(deliveryRepositoryMock.Object, driverRepositoryMock.Object);

        var result = await handler.Handle(
            new AssignDriverCommand(deliveryId, driverId.Value),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_DeliveryNotFound()
    {
        var deliveryRepositoryMock = new Mock<IDeliveryRepository>();
        var driverRepositoryMock = new Mock<IDriverRepository>();

        var deliveryId = Guid.NewGuid();
        var unknownDriverId = DriverId.New();

        driverRepositoryMock.Setup(r => r.GetByIdAsync(unknownDriverId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Driver.Create(
                "Jane Smith",
                "DL-789",
                PhoneNumber.Create("+15551234567"),
                Email.Create("jane@example.com")));

        var handler = new AssignDriverCommandHandler(deliveryRepositoryMock.Object, driverRepositoryMock.Object);

        var result = await handler.Handle(
            new AssignDriverCommand(deliveryId, unknownDriverId.Value),
            CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_DriverNotFound()
    {
        var deliveryRepositoryMock = new Mock<IDeliveryRepository>();
        var driverRepositoryMock = new Mock<IDriverRepository>();

        var deliveryId = Guid.NewGuid();
        var delivery = Delivery.Create(
            TrackingNumber.New(),
            "John Doe",
            "123 Main St",
            DateTime.UtcNow.AddDays(1));

        deliveryRepositoryMock.Setup(r => r.GetByIdAsync(deliveryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(delivery);

        driverRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<DriverId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Driver?)null);

        var handler = new AssignDriverCommandHandler(deliveryRepositoryMock.Object, driverRepositoryMock.Object);

        var result = await handler.Handle(
            new AssignDriverCommand(deliveryId, DriverId.New().Value),
            CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
}
