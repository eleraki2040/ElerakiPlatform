using Eleraki.WorkflowEngine.Domain;
using FluentAssertions;
using Xunit;

namespace Eleraki.WorkflowEngine.Tests;

public class WorkflowTests
{
    [Fact]
    public void Create_Should_Return_Workflow_With_Active_Status()
    {
        var workflow = Workflow.Create("Test Workflow", "Description");

        workflow.Name.Should().Be("Test Workflow");
        workflow.Description.Should().Be("Description");
        workflow.Status.Should().Be(WorkflowStatus.Active);
        workflow.Id.Should().NotBe(Guid.Empty);
        workflow.CreatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Activate_Should_Set_Status_To_Active()
    {
        var workflow = Workflow.Create("Test");

        workflow.Deactivate();
        workflow.Activate();

        workflow.Status.Should().Be(WorkflowStatus.Active);
    }

    [Fact]
    public void Complete_Should_Set_Status_To_Completed()
    {
        var workflow = Workflow.Create("Test");

        workflow.Complete();

        workflow.Status.Should().Be(WorkflowStatus.Completed);
    }
}
