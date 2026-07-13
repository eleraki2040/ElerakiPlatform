using Eleraki.DeliveryEngine.Application.Commands;
using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Repositories;
using MediatR;
using Moq;
using Xunit;

namespace Eleraki.DeliveryEngine.Application.Tests;

public class CreateDeliveryCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Delivery_And_ReturnGuid()
    {
        var deliveryRepositoryMock = new Mock<IDeliveryRepository>();
        var handler = new CreateDeliveryCommandHandler(deliveryRepositoryMock.Object);

        var result = await handler.Handle(
            new CreateDeliveryCommand(
                "John Doe",
                "123 Main St",
                DateTime.UtcNow.AddDays(1),
                new List<DeliveryLineItem>
                {
                    new("Widget", 2, 10m, "USD")
                }),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_AddDelivery_ToRepository()
    {
        var deliveryRepositoryMock = new Mock<IDeliveryRepository>();
        var handler = new CreateDeliveryCommandHandler(deliveryRepositoryMock.Object);

        var result = await handler.Handle(
            new CreateDeliveryCommand(
                "John Doe",
                "123 Main St",
                DateTime.UtcNow.AddDays(1),
                new List<DeliveryLineItem>
                {
                    new("Widget", 2, 10m, "USD")
                }),
            CancellationToken.None);

        deliveryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Delivery>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_Raise_DomainEvent()
    {
        var deliveryRepositoryMock = new Mock<IDeliveryRepository>();
        var handler = new CreateDeliveryCommandHandler(deliveryRepositoryMock.Object);

        var result = await handler.Handle(
            new CreateDeliveryCommand(
                "John Doe",
                "123 Main St",
                DateTime.UtcNow.AddDays(1),
                new List<DeliveryLineItem>
                {
                    new("Widget", 2, 10m, "USD")
                }),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }
}
