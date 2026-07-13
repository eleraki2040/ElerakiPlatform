using Eleraki.SchoolManagementEngine.Domain.Students;
using FluentAssertions;

namespace Eleraki.SchoolManagementEngine.Domain.Tests;

public class StudentTests
{
    [Fact]
    public void Create_Should_Return_Student_With_Active_Status()
    {
        var student = Student.Create("John", "Doe", "john@eleraki.com", new DateTime(2000, 1, 1), "123 Main St", "555-0100");

        student.Should().NotBeNull();
        student.FirstName.Should().Be("John");
        student.LastName.Should().Be("Doe");
        student.Email.Should().Be("john@eleraki.com");
        student.Address.Should().Be("123 Main St");
        student.PhoneNumber.Should().Be("555-0100");
        student.IsActive.Should().BeTrue();
        student.Id.Should().NotBeEquivalentTo(default(StudentId));
    }

    [Fact]
    public void Create_Should_Raise_StudentEnrolledDomainEvent()
    {
        var student = Student.Create("John", "Doe", "john@eleraki.com", new DateTime(2000, 1, 1), "123 Main St", "555-0100");

        student.DomainEvents.Should().Contain(e => e.GetType().Name == "StudentEnrolledDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_FirstName_Is_Null()
    {
        Action act = () => Student.Create(null!, "Doe", "john@eleraki.com", new DateTime(2000, 1, 1), "123 Main St", "555-0100");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Email_Is_Null()
    {
        Action act = () => Student.Create("John", "Doe", null!, new DateTime(2000, 1, 1), "123 Main St", "555-0100");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Deactivate_Should_Set_Inactive_Status()
    {
        var student = Student.Create("John", "Doe", "john@eleraki.com", new DateTime(2000, 1, 1), "123 Main St", "555-0100");

        student.Deactivate();

        student.IsActive.Should().BeFalse();
    }

    [Fact]
    public void Activate_Should_Set_Active_Status()
    {
        var student = Student.Create("John", "Doe", "john@eleraki.com", new DateTime(2000, 1, 1), "123 Main St", "555-0100");
        student.Deactivate();

        student.Activate();

        student.IsActive.Should().BeTrue();
    }
}
