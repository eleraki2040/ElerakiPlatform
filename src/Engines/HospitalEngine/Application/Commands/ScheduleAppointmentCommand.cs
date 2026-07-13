using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.HospitalEngine.Application.Commands;

public record ScheduleAppointmentCommand(Guid PatientId, Guid DoctorId, DateTime ScheduledAt, string? Notes = null) : IRequest<Result<Guid>>;

public class ScheduleAppointmentCommandHandler : IRequestHandler<ScheduleAppointmentCommand, Result<Guid>>
{
    private readonly IHospitalRepository _repository;

    public ScheduleAppointmentCommandHandler(IHospitalRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(ScheduleAppointmentCommand request, CancellationToken cancellationToken)
    {
        var patientId = PatientId.From(request.PatientId);
        var doctorId = DoctorId.From(request.DoctorId);

        var appointment = Appointment.Create(patientId, doctorId, request.ScheduledAt, request.Notes);

        await _repository.AddAppointmentAsync(appointment, cancellationToken);

        return Result<Guid>.Success(appointment.Id.Value);
    }
}
