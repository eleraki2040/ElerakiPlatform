using Eleraki.WorkflowEngine.Domain;
using Eleraki.WorkflowEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.WorkflowEngine.Application.Workflows.Commands;

public sealed record ActivateWorkflowCommand(WorkflowId WorkflowId) : IRequest;

public sealed class ActivateWorkflowCommandHandler : IRequestHandler<ActivateWorkflowCommand>
{
    private readonly IWorkflowRepository _workflowRepository;

    public ActivateWorkflowCommandHandler(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    public async Task Handle(ActivateWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = await _workflowRepository.GetByIdAsync(request.WorkflowId, cancellationToken);
        if (workflow is null)
            throw new Exception("Workflow not found.");

        workflow.Activate();

        await _workflowRepository.UpdateAsync(workflow, cancellationToken);
    }
}
