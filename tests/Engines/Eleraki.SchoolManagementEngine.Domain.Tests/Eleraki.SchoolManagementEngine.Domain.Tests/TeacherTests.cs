using Eleraki.SchoolManagementEngine.Domain.Teachers;
using FluentAssertions;

namespace Eleraki.SchoolManagementEngine.Domain.Tests;

public class TeacherTests
{
    [Fact]
    public void Create_Should_Return_Teacher_With_Active_Status()
    {
        var teacher = Teacher.Create("Jane", "Smith", "jane@eleraki.com", "555-0200", "Mathematics");

        teacher.Should().NotBeNull();
        teacher.FirstName.Should().Be("Jane");
        teacher.LastName.Should().Be("Smith");
        teacher.Email.Should().Be("jane@eleraki.com");
        teacher.PhoneNumber.Should().Be("555-0200");
        teacher.Specialization.Should().Be("Mathematics");
        teacher.IsActive.Should().BeTrue();
        teacher.Id.Should().NotBeEquivalentTo(default(TeacherId));
    }

    [Fact]
    public void Create_Should_Raise_TeacherHiredDomainEvent()
    {
        var teacher = Teacher.Create("Jane", "Smith", "jane@eleraki.com", "555-0200", "Mathematics");

        teacher.DomainEvents.Should().Contain(e => e.GetType().Name == "TeacherHiredDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_FirstName_Is_Null()
    {
        Action act = () => Teacher.Create(null!, "Smith", "jane@eleraki.com", "555-0200", "Mathematics");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Email_Is_Null()
    {
        Action act = () => Teacher.Create("Jane", "Smith", null!, "555-0200", "Mathematics");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Deactivate_Should_Set_Inactive_Status()
    {
        var teacher = Teacher.Create("Jane", "Smith", "jane@eleraki.com", "555-0200", "Mathematics");

        teacher.Deactivate();

        teacher.IsActive.Should().BeFalse();
    }

    [Fact]
    public void Activate_Should_Set_Active_Status()
    {
        var teacher = Teacher.Create("Jane", "Smith", "jane@eleraki.com", "555-0200", "Mathematics");
        teacher.Deactivate();

        teacher.Activate();

        teacher.IsActive.Should().BeTrue();
    }
}
