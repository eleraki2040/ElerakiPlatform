using Eleraki.WorkflowEngine.Domain;
using Eleraki.WorkflowEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.WorkflowEngine.Application.Workflows.Commands;

public sealed record CompleteWorkflowCommand(WorkflowId WorkflowId) : IRequest;

public sealed class CompleteWorkflowCommandHandler : IRequestHandler<CompleteWorkflowCommand>
{
    private readonly IWorkflowRepository _workflowRepository;

    public CompleteWorkflowCommandHandler(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    public async Task Handle(CompleteWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = await _workflowRepository.GetByIdAsync(request.WorkflowId, cancellationToken);
        if (workflow is null)
            throw new Exception("Workflow not found.");

        workflow.Complete();

        await _workflowRepository.UpdateAsync(workflow, cancellationToken);
    }
}
