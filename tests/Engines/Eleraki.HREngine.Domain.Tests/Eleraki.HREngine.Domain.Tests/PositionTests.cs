using Eleraki.HREngine.Domain;
using FluentAssertions;

namespace Eleraki.HREngine.Domain.Tests;

public class PositionTests
{
    [Fact]
    public void Create_Should_Return_Position_With_Active_Status()
    {
        var position = Position.Create("Software Engineer", "DEPT-001", "Develops software");

        position.Should().NotBeNull();
        position.Title.Should().Be("Software Engineer");
        position.Description.Should().Be("Develops software");
        position.DepartmentId.Should().Be("DEPT-001");
        position.Status.Should().Be(PositionStatus.Active);
        position.Id.Should().NotBeEquivalentTo(default(PositionId));
    }

    [Fact]
    public void Create_Should_Return_Position_With_Null_Description_When_Not_Provided()
    {
        var position = Position.Create("Software Engineer", "DEPT-001");

        position.Title.Should().Be("Software Engineer");
        position.Description.Should().BeNull();
    }

    [Fact]
    public void Create_Should_Trim_Title_And_Description()
    {
        var position = Position.Create("  Software Engineer  ", "  DEPT-001  ", "  Description  ");

        position.Title.Should().Be("Software Engineer");
        position.Description.Should().Be("Description");
    }

    [Fact]
    public void Create_Should_Raise_PositionCreatedDomainEvent()
    {
        var position = Position.Create("Software Engineer", "DEPT-001");

        position.DomainEvents.Should().Contain(e => e.GetType().Name == "PositionCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_Title_Is_Null()
    {
        Action act = () => Position.Create(null!, "DEPT-001");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_DepartmentId_Is_Null()
    {
        Action act = () => Position.Create("Software Engineer", null!);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Activate_Should_Set_Active_Status()
    {
        var position = Position.Create("Software Engineer", "DEPT-001");
        position.Deactivate();

        position.Activate();

        position.Status.Should().Be(PositionStatus.Active);
    }

    [Fact]
    public void Activate_Should_Be_Idempotent_When_Already_Active()
    {
        var position = Position.Create("Software Engineer", "DEPT-001");

        position.Activate();

        position.Status.Should().Be(PositionStatus.Active);
    }

    [Fact]
    public void Deactivate_Should_Set_Inactive_Status()
    {
        var position = Position.Create("Software Engineer", "DEPT-001");

        position.Deactivate();

        position.Status.Should().Be(PositionStatus.Inactive);
    }

    [Fact]
    public void Deactivate_Should_Be_Idempotent_When_Already_Inactive()
    {
        var position = Position.Create("Software Engineer", "DEPT-001");
        position.Deactivate();

        position.Deactivate();

        position.Status.Should().Be(PositionStatus.Inactive);
    }
}
