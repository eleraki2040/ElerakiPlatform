using Eleraki.WorkflowEngine.Domain;
using Eleraki.WorkflowEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.WorkflowEngine.Application.Workflows.Commands;

public sealed record CancelWorkflowCommand(WorkflowId WorkflowId) : IRequest;

public sealed class CancelWorkflowCommandHandler : IRequestHandler<CancelWorkflowCommand>
{
    private readonly IWorkflowRepository _workflowRepository;

    public CancelWorkflowCommandHandler(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    public async Task Handle(CancelWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = await _workflowRepository.GetByIdAsync(request.WorkflowId, cancellationToken);
        if (workflow is null)
            throw new Exception("Workflow not found.");

        workflow.Cancel();

        await _workflowRepository.UpdateAsync(workflow, cancellationToken);
    }
}
