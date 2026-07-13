using Eleraki.HREngine.Application.Commands;
using FluentValidation;

namespace Eleraki.HREngine.Application.Validators;

public class RecordAttendanceCommandValidator : AbstractValidator<RecordAttendanceCommand>
{
    public RecordAttendanceCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.AttendanceDate)
            .LessThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Attendance date cannot be in the future.");
    }
}
