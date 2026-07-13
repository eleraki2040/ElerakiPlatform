using Eleraki.HREngine.Domain;
using FluentAssertions;

namespace Eleraki.HREngine.Domain.Tests;

public class LeaveTests
{
    [Fact]
    public void Request_Should_Return_Leave_With_Pending_Status()
    {
        var leave = Leave.Request("EMP-001", LeaveType.Annual, new DateTime(2025, 6, 1), new DateTime(2025, 6, 5), "Vacation");

        leave.Should().NotBeNull();
        leave.EmployeeId.Should().Be("EMP-001");
        leave.Type.Should().Be(LeaveType.Annual);
        leave.StartDate.Should().Be(new DateTime(2025, 6, 1));
        leave.EndDate.Should().Be(new DateTime(2025, 6, 5));
        leave.Reason.Should().Be("Vacation");
        leave.Status.Should().Be(LeaveStatus.Pending);
        leave.ApprovedBy.Should().BeNull();
        leave.ApprovedAt.Should().BeNull();
        leave.Id.Should().NotBeEquivalentTo(default(LeaveId));
    }

    [Fact]
    public void Request_Should_Raise_LeaveRequestedDomainEvent()
    {
        var leave = Leave.Request("EMP-001", LeaveType.Annual, new DateTime(2025, 6, 1), new DateTime(2025, 6, 5));

        leave.DomainEvents.Should().Contain(e => e.GetType().Name == "LeaveRequestedDomainEvent");
    }

    [Fact]
    public void Request_Should_Throw_When_EmployeeId_Is_Null()
    {
        Action act = () => Leave.Request(null!, LeaveType.Annual, new DateTime(2025, 6, 1), new DateTime(2025, 6, 5));

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Approve_Should_Set_Approved_Status()
    {
        var leave = Leave.Request("EMP-001", LeaveType.Sick, new DateTime(2025, 6, 1), new DateTime(2025, 6, 3), "Flu");

        leave.Approve("Manager-001");

        leave.Status.Should().Be(LeaveStatus.Approved);
        leave.ApprovedBy.Should().Be("Manager-001");
        leave.ApprovedAt.Should().NotBeNull();
    }

    [Fact]
    public void Approve_Should_Raise_LeaveApprovedDomainEvent()
    {
        var leave = Leave.Request("EMP-001", LeaveType.Annual, new DateTime(2025, 6, 1), new DateTime(2025, 6, 5));

        leave.Approve("Manager-001");

        leave.DomainEvents.Should().Contain(e => e.GetType().Name == "LeaveApprovedDomainEvent");
    }

    [Fact]
    public void Approve_Should_Be_Ignored_When_Already_Approved()
    {
        var leave = Leave.Request("EMP-001", LeaveType.Annual, new DateTime(2025, 6, 1), new DateTime(2025, 6, 5));
        leave.Approve("Manager-001");

        leave.Approve("Manager-002");

        leave.Status.Should().Be(LeaveStatus.Approved);
        leave.ApprovedBy.Should().Be("Manager-001");
    }

    [Fact]
    public void Reject_Should_Set_Rejected_Status()
    {
        var leave = Leave.Request("EMP-001", LeaveType.Annual, new DateTime(2025, 6, 1), new DateTime(2025, 6, 5), "Unplanned");

        leave.Reject("Manager-001");

        leave.Status.Should().Be(LeaveStatus.Rejected);
        leave.ApprovedBy.Should().Be("Manager-001");
        leave.ApprovedAt.Should().NotBeNull();
    }

    [Fact]
    public void Reject_Should_Raise_LeaveRejectedDomainEvent()
    {
        var leave = Leave.Request("EMP-001", LeaveType.Annual, new DateTime(2025, 6, 1), new DateTime(2025, 6, 5));

        leave.Reject("Manager-001");

        leave.DomainEvents.Should().Contain(e => e.GetType().Name == "LeaveRejectedDomainEvent");
    }

    [Fact]
    public void Reject_Should_Be_Ignored_When_Already_Approved()
    {
        var leave = Leave.Request("EMP-001", LeaveType.Annual, new DateTime(2025, 6, 1), new DateTime(2025, 6, 5));
        leave.Approve("Manager-001");

        leave.Reject("Manager-002");

        leave.Status.Should().Be(LeaveStatus.Approved);
    }
}
