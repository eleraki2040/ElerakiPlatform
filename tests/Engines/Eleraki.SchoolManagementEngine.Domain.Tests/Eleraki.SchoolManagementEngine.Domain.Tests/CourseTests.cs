using Eleraki.SchoolManagementEngine.Domain.Courses;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using FluentAssertions;

namespace Eleraki.SchoolManagementEngine.Domain.Tests;

public class CourseTests
{
    [Fact]
    public void Create_Should_Return_Course_With_Active_Status()
    {
        var teacherId = TeacherId.New();
        var course = Course.Create("Mathematics", "MATH101", "Advanced Mathematics", 4, teacherId);

        course.Should().NotBeNull();
        course.Name.Should().Be("Mathematics");
        course.Code.Should().Be("MATH101");
        course.Description.Should().Be("Advanced Mathematics");
        course.Credits.Should().Be(4);
        course.TeacherId.Should().Be(teacherId);
        course.IsActive.Should().BeTrue();
        course.Id.Should().NotBeEquivalentTo(default(CourseId));
    }

    [Fact]
    public void Create_Should_Raise_CourseCreatedDomainEvent()
    {
        var teacherId = TeacherId.New();
        var course = Course.Create("Mathematics", "MATH101", "Advanced Mathematics", 4, teacherId);

        course.DomainEvents.Should().Contain(e => e.GetType().Name == "CourseCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Null()
    {
        var teacherId = TeacherId.New();
        Action act = () => Course.Create(null!, "MATH101", "Advanced Mathematics", 4, teacherId);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Update_Should_Change_Properties()
    {
        var teacherId = TeacherId.New();
        var course = Course.Create("Mathematics", "MATH101", "Advanced Mathematics", 4, teacherId);

        course.Update("Physics", "Introductory Physics", 3);

        course.Name.Should().Be("Physics");
        course.Description.Should().Be("Introductory Physics");
        course.Credits.Should().Be(3);
    }

    [Fact]
    public void Deactivate_Should_Set_Inactive_Status()
    {
        var teacherId = TeacherId.New();
        var course = Course.Create("Mathematics", "MATH101", "Advanced Mathematics", 4, teacherId);

        course.Deactivate();

        course.IsActive.Should().BeFalse();
    }
}
