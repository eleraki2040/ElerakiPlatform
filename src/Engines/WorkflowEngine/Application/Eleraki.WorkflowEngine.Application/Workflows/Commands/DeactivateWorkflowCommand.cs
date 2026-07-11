using Eleraki.WorkflowEngine.Domain;
using Eleraki.WorkflowEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.WorkflowEngine.Application.Workflows.Commands;

public sealed record DeactivateWorkflowCommand(WorkflowId WorkflowId) : IRequest;

public sealed class DeactivateWorkflowCommandHandler : IRequestHandler<DeactivateWorkflowCommand>
{
    private readonly IWorkflowRepository _workflowRepository;

    public DeactivateWorkflowCommandHandler(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    public async Task Handle(DeactivateWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = await _workflowRepository.GetByIdAsync(request.WorkflowId, cancellationToken);
        if (workflow is null)
            throw new Exception("Workflow not found.");

        workflow.Deactivate();

        await _workflowRepository.UpdateAsync(workflow, cancellationToken);
    }
}
