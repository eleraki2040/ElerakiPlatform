using Eleraki.SchoolManagementEngine.Domain.Classes;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using FluentAssertions;

namespace Eleraki.SchoolManagementEngine.Domain.Tests;

public class ClassTests
{
    [Fact]
    public void Create_Should_Return_Class_With_Active_Status()
    {
        var teacherId = TeacherId.New();
        var classEntity = Class.Create("Class A", "10th", teacherId, 30);

        classEntity.Should().NotBeNull();
        classEntity.Name.Should().Be("Class A");
        classEntity.Grade.Should().Be("10th");
        classEntity.HomeroomTeacherId.Should().Be(teacherId);
        classEntity.MaxCapacity.Should().Be(30);
        classEntity.IsActive.Should().BeTrue();
        classEntity.Id.Should().NotBeEquivalentTo(default(ClassId));
    }

    [Fact]
    public void Create_Should_Raise_ClassCreatedDomainEvent()
    {
        var teacherId = TeacherId.New();
        var classEntity = Class.Create("Class A", "10th", teacherId, 30);

        classEntity.DomainEvents.Should().Contain(e => e.GetType().Name == "ClassCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Null()
    {
        var teacherId = TeacherId.New();
        Action act = () => Class.Create(null!, "10th", teacherId, 30);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Update_Should_Change_Properties()
    {
        var teacherId = TeacherId.New();
        var classEntity = Class.Create("Class A", "10th", teacherId, 30);

        classEntity.Update("Class B", "11th", 35);

        classEntity.Name.Should().Be("Class B");
        classEntity.Grade.Should().Be("11th");
        classEntity.MaxCapacity.Should().Be(35);
    }

    [Fact]
    public void Deactivate_Should_Set_Inactive_Status()
    {
        var teacherId = TeacherId.New();
        var classEntity = Class.Create("Class A", "10th", teacherId, 30);

        classEntity.Deactivate();

        classEntity.IsActive.Should().BeFalse();
    }
}
