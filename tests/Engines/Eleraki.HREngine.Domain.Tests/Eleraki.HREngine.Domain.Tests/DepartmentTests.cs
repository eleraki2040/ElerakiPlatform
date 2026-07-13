using Eleraki.HREngine.Domain;
using FluentAssertions;

namespace Eleraki.HREngine.Domain.Tests;

public class DepartmentTests
{
    [Fact]
    public void Create_Should_Return_Department_With_Active_Status()
    {
        var department = Department.Create("Engineering", "Technology division");

        department.Should().NotBeNull();
        department.Name.Should().Be("Engineering");
        department.Description.Should().Be("Technology division");
        department.Status.Should().Be(DepartmentStatus.Active);
        department.Id.Should().NotBeEquivalentTo(default(DepartmentId));
    }

    [Fact]
    public void Create_Should_Return_Department_With_Null_Description_When_Not_Provided()
    {
        var department = Department.Create("Engineering");

        department.Name.Should().Be("Engineering");
        department.Description.Should().BeNull();
    }

    [Fact]
    public void Create_Should_Trim_Name()
    {
        var department = Department.Create("  Engineering  ");

        department.Name.Should().Be("Engineering");
    }

    [Fact]
    public void Create_Should_Raise_DepartmentCreatedDomainEvent()
    {
        var department = Department.Create("Engineering");

        department.DomainEvents.Should().Contain(e => e.GetType().Name == "DepartmentCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Null()
    {
        Action act = () => Department.Create(null!);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Activate_Should_Set_Active_Status()
    {
        var department = Department.Create("Engineering");
        department.Deactivate();

        department.Activate();

        department.Status.Should().Be(DepartmentStatus.Active);
    }

    [Fact]
    public void Activate_Should_Be_Idempotent_When_Already_Active()
    {
        var department = Department.Create("Engineering");

        department.Activate();

        department.Status.Should().Be(DepartmentStatus.Active);
    }

    [Fact]
    public void Deactivate_Should_Set_Inactive_Status()
    {
        var department = Department.Create("Engineering");

        department.Deactivate();

        department.Status.Should().Be(DepartmentStatus.Inactive);
    }

    [Fact]
    public void Deactivate_Should_Be_Idempotent_When_Already_Inactive()
    {
        var department = Department.Create("Engineering");
        department.Deactivate();

        department.Deactivate();

        department.Status.Should().Be(DepartmentStatus.Inactive);
    }
}
