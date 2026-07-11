using Eleraki.WorkflowEngine.Application.Workflows.Commands;
using Eleraki.WorkflowEngine.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.WorkflowEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkflowsController : ControllerBase
{
    private readonly ISender _sender;

    public WorkflowsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateWorkflowRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateWorkflowCommand(request.Name, request.Description);
        var workflowId = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = workflowId }, workflowId);
    }

    [HttpPut("{id:guid}/update")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWorkflowRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateWorkflowCommand(WorkflowId.From(id), request.Name, request.Description);
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteWorkflowCommand(WorkflowId.From(id));
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        var command = new ActivateWorkflowCommand(WorkflowId.From(id));
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeactivateWorkflowCommand(WorkflowId.From(id));
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
    {
        var command = new CompleteWorkflowCommand(WorkflowId.From(id));
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        var command = new CancelWorkflowCommand(WorkflowId.From(id));
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }
}

public sealed record CreateWorkflowRequest(string Name, string? Description = null);
public sealed record UpdateWorkflowRequest(string Name, string? Description = null);
