using Eleraki.PurchasingEngine.Application.Commands;
using Eleraki.PurchasingEngine.Domain.Entities;
using Eleraki.PurchasingEngine.Domain.Repositories;
using Eleraki.PurchasingEngine.Domain.ValueObjects;
using Eleraki.SharedKernel.Primitives;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Eleraki.PurchasingEngine.Application.Tests;

public class CreatePurchaseOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenVendorExists_ShouldCreatePurchaseOrder()
    {
        var vendorId = VendorId.New();
        var vendor = Vendor.Create("Acme Corp");
        var vendorRepositoryMock = new Mock<IVendorRepository>();
        var purchaseOrderRepositoryMock = new Mock<IPurchaseOrderRepository>();
        vendorRepositoryMock.Setup(r => r.GetByIdAsync(vendorId, It.IsAny<CancellationToken>())).ReturnsAsync(vendor);

        var handler = new CreatePurchaseOrderCommandHandler(purchaseOrderRepositoryMock.Object, vendorRepositoryMock.Object);
        var command = new CreatePurchaseOrderCommand(vendorId.Value);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_WhenVendorNotFound_ShouldReturnFailure()
    {
        var vendorRepositoryMock = new Mock<IVendorRepository>();
        var purchaseOrderRepositoryMock = new Mock<IPurchaseOrderRepository>();
        vendorRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<VendorId>(), It.IsAny<CancellationToken>())).ReturnsAsync((Vendor?)null);

        var handler = new CreatePurchaseOrderCommandHandler(purchaseOrderRepositoryMock.Object, vendorRepositoryMock.Object);
        var command = new CreatePurchaseOrderCommand(Guid.NewGuid());

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }
}
