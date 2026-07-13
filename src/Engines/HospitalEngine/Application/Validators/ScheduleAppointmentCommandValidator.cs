using Eleraki.HospitalEngine.Application.Commands;
using FluentValidation;

namespace Eleraki.HospitalEngine.Application.Validators;

public class ScheduleAppointmentCommandValidator : AbstractValidator<ScheduleAppointmentCommand>
{
    public ScheduleAppointmentCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("Patient ID is required.");

        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("Doctor ID is required.");

        RuleFor(x => x.ScheduledAt)
            .GreaterThan(DateTime.UtcNow).WithMessage("Scheduled date must be in the future.");
    }
}
