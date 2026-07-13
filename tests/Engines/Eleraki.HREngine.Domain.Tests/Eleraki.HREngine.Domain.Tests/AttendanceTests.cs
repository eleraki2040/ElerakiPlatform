using Eleraki.HREngine.Domain;
using FluentAssertions;

namespace Eleraki.HREngine.Domain.Tests;

public class AttendanceTests
{
    [Fact]
    public void Record_Should_Return_Attendance_With_Present_Status()
    {
        var attendance = Attendance.Record("EMP-001", new DateTime(2025, 1, 15), new DateTime(2025, 1, 15, 9, 0, 0), "On time");

        attendance.Should().NotBeNull();
        attendance.EmployeeId.Should().Be("EMP-001");
        attendance.AttendanceDate.Should().Be(new DateTime(2025, 1, 15));
        attendance.CheckInTime.Should().Be(new DateTime(2025, 1, 15, 9, 0, 0));
        attendance.Status.Should().Be(AttendanceStatus.Present);
        attendance.Notes.Should().Be("On time");
        attendance.Id.Should().NotBeEquivalentTo(default(AttendanceId));
    }

    [Fact]
    public void Record_Should_Set_Null_CheckInTime_When_Not_Provided()
    {
        var attendance = Attendance.Record("EMP-001", new DateTime(2025, 1, 15), notes: "Absent");

        attendance.CheckInTime.Should().BeNull();
        attendance.Notes.Should().Be("Absent");
    }

    [Fact]
    public void Record_Should_Raise_AttendanceRecordedDomainEvent()
    {
        var attendance = Attendance.Record("EMP-001", new DateTime(2025, 1, 15));

        attendance.DomainEvents.Should().Contain(e => e.GetType().Name == "AttendanceRecordedDomainEvent");
    }

    [Fact]
    public void Record_Should_Normalize_AttendanceDate_To_Date_Only()
    {
        var attendance = Attendance.Record("EMP-001", new DateTime(2025, 1, 15, 14, 30, 0));

        attendance.AttendanceDate.Should().Be(new DateTime(2025, 1, 15));
    }

    [Fact]
    public void CheckOut_Should_Set_CheckOutTime()
    {
        var attendance = Attendance.Record("EMP-001", new DateTime(2025, 1, 15));

        attendance.CheckOut(new DateTime(2025, 1, 15, 17, 0, 0));

        attendance.CheckOutTime.Should().Be(new DateTime(2025, 1, 15, 17, 0, 0));
    }

    [Fact]
    public void CheckOut_Should_Raise_AttendanceCheckedOutDomainEvent()
    {
        var attendance = Attendance.Record("EMP-001", new DateTime(2025, 1, 15));

        attendance.CheckOut(new DateTime(2025, 1, 15, 17, 0, 0));

        attendance.DomainEvents.Should().Contain(e => e.GetType().Name == "AttendanceCheckedOutDomainEvent");
    }

    [Fact]
    public void MarkAbsent_Should_Set_Absent_Status()
    {
        var attendance = Attendance.Record("EMP-001", new DateTime(2025, 1, 15));

        attendance.MarkAbsent("Family emergency");

        attendance.Status.Should().Be(AttendanceStatus.Absent);
        attendance.Notes.Should().Be("Family emergency");
    }

    [Fact]
    public void MarkAbsent_Should_Set_Null_Notes_When_Not_Provided()
    {
        var attendance = Attendance.Record("EMP-001", new DateTime(2025, 1, 15));

        attendance.MarkAbsent();

        attendance.Status.Should().Be(AttendanceStatus.Absent);
        attendance.Notes.Should().BeNull();
    }

    [Fact]
    public void Record_Should_Throw_When_EmployeeId_Is_Null()
    {
        Action act = () => Attendance.Record(null!, new DateTime(2025, 1, 15));

        act.Should().Throw<ArgumentException>();
    }
}
