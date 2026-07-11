using Eleraki.WorkflowEngine.Domain;
using Eleraki.WorkflowEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.WorkflowEngine.Application.Workflows.Commands;

public sealed record DeleteWorkflowCommand(WorkflowId WorkflowId) : IRequest;

public sealed class DeleteWorkflowCommandHandler : IRequestHandler<DeleteWorkflowCommand>
{
    private readonly IWorkflowRepository _workflowRepository;

    public DeleteWorkflowCommandHandler(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    public async Task Handle(DeleteWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = await _workflowRepository.GetByIdAsync(request.WorkflowId, cancellationToken);
        if (workflow is null)
            throw new Exception("Workflow not found.");

        workflow.Delete();

        await _workflowRepository.DeleteAsync(workflow, cancellationToken);
    }
}
