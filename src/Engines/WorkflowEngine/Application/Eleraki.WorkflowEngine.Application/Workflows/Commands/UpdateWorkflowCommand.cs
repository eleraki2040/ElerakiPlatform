using Eleraki.WorkflowEngine.Domain;
using Eleraki.WorkflowEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.WorkflowEngine.Application.Workflows.Commands;

public sealed record UpdateWorkflowCommand(WorkflowId WorkflowId, string Name, string? Description) : IRequest;

public sealed class UpdateWorkflowCommandHandler : IRequestHandler<UpdateWorkflowCommand>
{
    private readonly IWorkflowRepository _workflowRepository;

    public UpdateWorkflowCommandHandler(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    public async Task Handle(UpdateWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = await _workflowRepository.GetByIdAsync(request.WorkflowId, cancellationToken);
        if (workflow is null)
            throw new Exception("Workflow not found.");

        workflow.Update(request.Name, request.Description);

        await _workflowRepository.UpdateAsync(workflow, cancellationToken);
    }
}
