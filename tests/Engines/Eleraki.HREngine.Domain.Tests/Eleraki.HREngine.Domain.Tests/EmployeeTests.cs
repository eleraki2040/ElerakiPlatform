using Eleraki.HREngine.Domain;
using FluentAssertions;

namespace Eleraki.HREngine.Domain.Tests;

public class EmployeeTests
{
    [Fact]
    public void Create_Should_Return_Employee_With_Active_Status()
    {
        var employee = Employee.Create(
            "John", "Doe", "john@eleraki.com", "555-0100",
            new DateTime(1990, 5, 15), "DEPT-001", "POS-001");

        employee.Should().NotBeNull();
        employee.FirstName.Should().Be("John");
        employee.LastName.Should().Be("Doe");
        employee.Email.Should().Be("john@eleraki.com");
        employee.Phone.Should().Be("555-0100");
        employee.DepartmentId.Should().Be("DEPT-001");
        employee.PositionId.Should().Be("POS-001");
        employee.Status.Should().Be(EmployeeStatus.Active);
        employee.Id.Should().NotBeEquivalentTo(default(EmployeeId));
    }

    [Fact]
    public void Create_Should_Trim_Input_Fields()
    {
        var employee = Employee.Create(
            "  John  ", "  Doe  ", "  john@eleraki.com  ", "  555-0100  ",
            new DateTime(1990, 5, 15), "  DEPT-001  ", "  POS-001  ");

        employee.FirstName.Should().Be("John");
        employee.LastName.Should().Be("Doe");
        employee.Email.Should().Be("john@eleraki.com");
        employee.Phone.Should().Be("555-0100");
        employee.DepartmentId.Should().Be("DEPT-001");
        employee.PositionId.Should().Be("POS-001");
    }

    [Fact]
    public void Create_Should_Raise_EmployeeCreatedDomainEvent()
    {
        var employee = Employee.Create(
            "John", "Doe", "john@eleraki.com", "555-0100",
            new DateTime(1990, 5, 15), "DEPT-001", "POS-001");

        employee.DomainEvents.Should().Contain(e => e.GetType().Name == "EmployeeCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_FirstName_Is_Null()
    {
        Action act = () => Employee.Create(null!, "Doe", "john@eleraki.com", "555-0100", new DateTime(1990, 5, 15), "DEPT-001", "POS-001");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Email_Is_Null()
    {
        Action act = () => Employee.Create("John", "Doe", null!, "555-0100", new DateTime(1990, 5, 15), "DEPT-001", "POS-001");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Activate_Should_Set_Active_Status()
    {
        var employee = Employee.Create(
            "John", "Doe", "john@eleraki.com", "555-0100",
            new DateTime(1990, 5, 15), "DEPT-001", "POS-001");
        employee.Deactivate();

        employee.Activate();

        employee.Status.Should().Be(EmployeeStatus.Active);
    }

    [Fact]
    public void Activate_Should_Be_Idempotent_When_Already_Active()
    {
        var employee = Employee.Create(
            "John", "Doe", "john@eleraki.com", "555-0100",
            new DateTime(1990, 5, 15), "DEPT-001", "POS-001");

        employee.Activate();

        employee.Status.Should().Be(EmployeeStatus.Active);
    }

    [Fact]
    public void Deactivate_Should_Set_Inactive_Status()
    {
        var employee = Employee.Create(
            "John", "Doe", "john@eleraki.com", "555-0100",
            new DateTime(1990, 5, 15), "DEPT-001", "POS-001");

        employee.Deactivate();

        employee.Status.Should().Be(EmployeeStatus.Inactive);
    }

    [Fact]
    public void Deactivate_Should_Be_Idempotent_When_Already_Inactive()
    {
        var employee = Employee.Create(
            "John", "Doe", "john@eleraki.com", "555-0100",
            new DateTime(1990, 5, 15), "DEPT-001", "POS-001");
        employee.Deactivate();

        employee.Deactivate();

        employee.Status.Should().Be(EmployeeStatus.Inactive);
    }
}
