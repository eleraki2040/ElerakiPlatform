using Eleraki.HospitalEngine.Application.DTOs;
using Eleraki.HospitalEngine.Application.Queries;
using Eleraki.HospitalEngine.Domain;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Repositories;
using Eleraki.SharedKernel.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.HospitalEngine.Application.Tests;

public class GetPatientByIdQueryHandlerTests
{
    private readonly Mock<IHospitalRepository> _repositoryMock;
    private readonly GetPatientByIdQueryHandler _handler;

    public GetPatientByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IHospitalRepository>();
        _handler = new GetPatientByIdQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Patient_Dto_When_Patient_Exists()
    {
        var patient = Patient.Create(
            PersonName.Create("John Doe"),
            Email.Create("john@example.com"),
            PhoneNumber.Create("123456789"),
            new DateTime(1990, 1, 1));

        _repositoryMock
            .Setup(repo => repo.GetPatientByIdAsync(It.IsAny<PatientId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Patient?)patient);

        var result = await _handler.Handle(
            new GetPatientByIdQuery(patient.Id.Value),
            CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(patient.Id.Value);
        result.Name.Should().Be("John Doe");
        result.Email.Should().Be("john@example.com");
        result.Phone.Should().Be("123456789");
        result.DateOfBirth.Should().Be(new DateTime(1990, 1, 1));
        result.Status.Should().Be("Active");
    }

    [Fact]
    public async Task Handle_Should_Return_Null_When_Patient_Does_Not_Exist()
    {
        _repositoryMock
            .Setup(repo => repo.GetPatientByIdAsync(It.IsAny<PatientId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Patient?)null);

        var result = await _handler.Handle(
            new GetPatientByIdQuery(Guid.NewGuid()),
            CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_Should_Call_Repository_GetPatientByIdAsync_With_Correct_Id()
    {
        var queryId = Guid.NewGuid();

        _repositoryMock
            .Setup(repo => repo.GetPatientByIdAsync(It.IsAny<PatientId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Patient?)null);

        await _handler.Handle(
            new GetPatientByIdQuery(queryId),
            CancellationToken.None);

        _repositoryMock.Verify(
            repo => repo.GetPatientByIdAsync(It.Is<PatientId>(id => id.Value == queryId), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
