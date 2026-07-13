using Eleraki.HospitalEngine.Application.Commands;
using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.Repositories;
using Eleraki.SharedKernel.ValueObjects;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.HospitalEngine.Application.Tests;

public class ScheduleAppointmentCommandHandlerTests
{
    private readonly Mock<IHospitalRepository> _repositoryMock;
    private readonly ScheduleAppointmentCommandHandler _handler;

    public ScheduleAppointmentCommandHandlerTests()
    {
        _repositoryMock = new Mock<IHospitalRepository>();
        _handler = new ScheduleAppointmentCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_With_Appointment_Id_When_Valid()
    {
        var result = await _handler.Handle(
            new ScheduleAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(1), "Checkup"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Handle_Should_Call_AddAppointmentAsync_Once()
    {
        var result = await _handler.Handle(
            new ScheduleAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(1)),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _repositoryMock.Verify(
            repo => repo.AddAppointmentAsync(It.IsAny<Appointment>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Raise_Appointment_Scheduled_Domain_Event()
    {
        Appointment? capturedAppointment = null;

        _repositoryMock
            .Setup(repo => repo.AddAppointmentAsync(It.IsAny<Appointment>(), It.IsAny<CancellationToken>()))
            .Callback<Appointment, CancellationToken>((appointment, _) => capturedAppointment = appointment)
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(
            new ScheduleAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(1)),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        capturedAppointment.Should().NotBeNull();
        capturedAppointment!.DomainEvents.Should().Contain(e => e is AppointmentScheduledDomainEvent);
    }
}
