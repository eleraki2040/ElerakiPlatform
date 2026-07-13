using Eleraki.DeliveryEngine.Application.DTOs;
using Eleraki.DeliveryEngine.Application.Queries;
using Eleraki.DeliveryEngine.Domain.Deliveries;
using Eleraki.DeliveryEngine.Domain.Repositories;
using Eleraki.SharedKernel.ValueObjects;
using MediatR;
using Moq;
using Xunit;

namespace Eleraki.DeliveryEngine.Application.Tests;

public class GetDeliveryByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_DeliveryDto_When_DeliveryExists()
    {
        var deliveryRepositoryMock = new Mock<IDeliveryRepository>();
        var handler = new GetDeliveryByIdQueryHandler(deliveryRepositoryMock.Object);

        var delivery = Delivery.Create(
            TrackingNumber.From("TRK-ABC123"),
            "John Doe",
            "123 Main St",
            DateTime.UtcNow.AddDays(1));

        deliveryRepositoryMock.Setup(r => r.GetByIdAsync(delivery.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(delivery);

        var result = await handler.Handle(
            new GetDeliveryByIdQuery(delivery.Id),
            CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(delivery.Id);
        result.TrackingNumber.Should().Be("TRK-ABC123");
        result.RecipientName.Should().Be("John Doe");
        result.Status.Should().Be("Pending");
    }

    [Fact]
    public async Task Handle_Should_ReturnNull_When_DeliveryNotFound()
    {
        var deliveryRepositoryMock = new Mock<IDeliveryRepository>();
        var handler = new GetDeliveryByIdQueryHandler(deliveryRepositoryMock.Object);

        var unknownId = Guid.NewGuid();
        deliveryRepositoryMock.Setup(r => r.GetByIdAsync(unknownId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Delivery?)null);

        var result = await handler.Handle(
            new GetDeliveryByIdQuery(unknownId),
            CancellationToken.None);

        result.Should().BeNull();
    }
}
