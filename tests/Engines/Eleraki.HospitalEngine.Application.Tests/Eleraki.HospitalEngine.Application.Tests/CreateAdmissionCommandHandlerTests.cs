using Eleraki.HospitalEngine.Application.Commands;
using Eleraki.HospitalEngine.Domain.Admissions;
using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Repositories;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.HospitalEngine.Application.Tests;

public class CreateAdmissionCommandHandlerTests
{
    private readonly Mock<IHospitalRepository> _repositoryMock;
    private readonly CreateAdmissionCommandHandler _handler;

    public CreateAdmissionCommandHandlerTests()
    {
        _repositoryMock = new Mock<IHospitalRepository>();
        _handler = new CreateAdmissionCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_With_Admission_Id_When_Valid()
    {
        var result = await _handler.Handle(
            new CreateAdmissionCommand(Guid.NewGuid(), null, "Observation needed"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Handle_Should_Call_AddAdmissionAsync_Once()
    {
        var result = await _handler.Handle(
            new CreateAdmissionCommand(Guid.NewGuid(), Guid.NewGuid(), "Observation needed"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _repositoryMock.Verify(
            repo => repo.AddAdmissionAsync(It.IsAny<Admission>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Raise_Admission_Created_Domain_Event()
    {
        Admission? capturedAdmission = null;

        _repositoryMock
            .Setup(repo => repo.AddAdmissionAsync(It.IsAny<Admission>(), It.IsAny<CancellationToken>()))
            .Callback<Admission, CancellationToken>((admission, _) => capturedAdmission = admission)
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(
            new CreateAdmissionCommand(Guid.NewGuid(), null, "Observation needed"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        capturedAdmission.Should().NotBeNull();
        capturedAdmission!.DomainEvents.Should().Contain(e => e is AdmissionCreatedDomainEvent);
    }
}
