using Eleraki.HospitalEngine.Application.Commands;
using Eleraki.HospitalEngine.Domain.Billing;
using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Repositories;
using Eleraki.SharedKernel.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.HospitalEngine.Application.Tests;

public class GenerateInvoiceCommandHandlerTests
{
    private readonly Mock<IHospitalRepository> _repositoryMock;
    private readonly GenerateInvoiceCommandHandler _handler;

    public GenerateInvoiceCommandHandlerTests()
    {
        _repositoryMock = new Mock<IHospitalRepository>();
        _handler = new GenerateInvoiceCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_With_Invoice_Id_When_Valid()
    {
        var result = await _handler.Handle(
            new GenerateInvoiceCommand(Guid.NewGuid(), 150.00m, "USD", DateTime.UtcNow.AddDays(30)),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Handle_Should_Call_AddInvoiceAsync_Once()
    {
        var result = await _handler.Handle(
            new GenerateInvoiceCommand(Guid.NewGuid(), 150.00m, "USD", DateTime.UtcNow.AddDays(30)),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _repositoryMock.Verify(
            repo => repo.AddInvoiceAsync(It.IsAny<Invoice>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Raise_Invoice_Generated_Domain_Event()
    {
        Invoice? capturedInvoice = null;

        _repositoryMock
            .Setup(repo => repo.AddInvoiceAsync(It.IsAny<Invoice>(), It.IsAny<CancellationToken>()))
            .Callback<Invoice, CancellationToken>((invoice, _) => capturedInvoice = invoice)
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(
            new GenerateInvoiceCommand(Guid.NewGuid(), 150.00m, "USD", DateTime.UtcNow.AddDays(30)),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        capturedInvoice.Should().NotBeNull();
        capturedInvoice!.DomainEvents.Should().Contain(e => e is InvoiceGeneratedDomainEvent);
    }
}
