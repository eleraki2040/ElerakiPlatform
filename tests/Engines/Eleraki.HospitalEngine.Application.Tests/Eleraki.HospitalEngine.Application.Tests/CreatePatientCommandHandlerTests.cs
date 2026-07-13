using Eleraki.HospitalEngine.Application.Commands;
using Eleraki.HospitalEngine.Domain;
using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.Repositories;
using Eleraki.SharedKernel.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.HospitalEngine.Application.Tests;

public class CreatePatientCommandHandlerTests
{
    private readonly Mock<IHospitalRepository> _repositoryMock;
    private readonly CreatePatientCommandHandler _handler;

    public CreatePatientCommandHandlerTests()
    {
        _repositoryMock = new Mock<IHospitalRepository>();
        _handler = new CreatePatientCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_With_Patient_Id_When_Valid()
    {
        var result = await _handler.Handle(
            new CreatePatientCommand("John Doe", "john@example.com", "123456789", new DateTime(1990, 1, 1)),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Handle_Should_Call_AddPatientAsync_With_Valid_Patient()
    {
        var result = await _handler.Handle(
            new CreatePatientCommand("John Doe", "john@example.com", "123456789", new DateTime(1990, 1, 1)),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _repositoryMock.Verify(
            repo => repo.AddPatientAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Raise_Patient_Registered_Domain_Event()
    {
        Patient? capturedPatient = null;

        _repositoryMock
            .Setup(repo => repo.AddPatientAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()))
            .Callback<Patient, CancellationToken>((patient, _) => capturedPatient = patient)
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(
            new CreatePatientCommand("John Doe", "john@example.com", "123456789", new DateTime(1990, 1, 1)),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        capturedPatient.Should().NotBeNull();
        capturedPatient!.DomainEvents.Should().Contain(e => e is PatientRegisteredDomainEvent);
    }
}
