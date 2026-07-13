using Eleraki.WorkflowEngine.Domain;
using Xunit;

namespace Eleraki.WorkflowEngine.Tests;

public class WorkflowTests
{
    [Fact]
    public void Create_Should_Return_Workflow_With_Active_Status()
    {
        var workflow = Workflow.Create("Onboarding", "Employee onboarding workflow");

        Assert.NotNull(workflow);
        Assert.Equal("Onboarding", workflow.Name);
        Assert.Equal("Employee onboarding workflow", workflow.Description);
        Assert.Equal(WorkflowStatus.Active, workflow.Status);
        Assert.NotEqual(default, workflow.Id);
    }

    [Fact]
    public void Create_Should_Raise_WorkflowCreatedDomainEvent()
    {
        var workflow = Workflow.Create("Onboarding");

        Assert.Contains(workflow.DomainEvents, e => e.GetType().Name == "WorkflowCreatedDomainEvent");
    }

    [Fact]
    public void Activate_Should_Set_Status_To_Active()
    {
        var workflow = Workflow.Create("Onboarding");
        workflow.Deactivate();

        workflow.Activate();

        Assert.Equal(WorkflowStatus.Active, workflow.Status);
    }

    [Fact]
    public void Deactivate_Should_Set_Status_To_Inactive()
    {
        var workflow = Workflow.Create("Onboarding");

        workflow.Deactivate();

        Assert.Equal(WorkflowStatus.Inactive, workflow.Status);
    }

    [Fact]
    public void Complete_Should_Set_Status_To_Completed()
    {
        var workflow = Workflow.Create("Onboarding");

        workflow.Complete();

        Assert.Equal(WorkflowStatus.Completed, workflow.Status);
    }

    [Fact]
    public void Cancel_Should_Set_Status_To_Cancelled()
    {
        var workflow = Workflow.Create("Onboarding");

        workflow.Cancel();

        Assert.Equal(WorkflowStatus.Cancelled, workflow.Status);
    }

    [Fact]
    public void Update_Should_Change_Name_and_description()
    {
        var workflow = Workflow.Create("Onboarding", "Old description");

        workflow.Update("New Onboarding", "New description");

        Assert.Equal("New Onboarding", workflow.Name);
        Assert.Equal("New description", workflow.Description);
    }
}
