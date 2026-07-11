using Eleraki.WorkflowEngine.Domain;
using Eleraki.WorkflowEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.WorkflowEngine.Application.Workflows.Commands;

/// <summary>
/// Command to create a new workflow.
/// </summary>
/// <param name="Name">The workflow name.</param>
/// <param name="Description">The workflow description.</param>
public sealed record CreateWorkflowCommand(string Name, string? Description = null) : IRequest<Guid>;

/// <summary>
/// Handler for CreateWorkflowCommand.
/// </summary>
public sealed class CreateWorkflowCommandHandler : IRequestHandler<CreateWorkflowCommand, Guid>
{
    private readonly IWorkflowRepository _workflowRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateWorkflowCommandHandler"/> class.
    /// </summary>
    /// <param name="workflowRepository">The workflow repository.</param>
    public CreateWorkflowCommandHandler(IWorkflowRepository workflowRepository)
    {
        _workflowRepository = workflowRepository;
    }

    /// <inheritdoc/>
    public async Task<Guid> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
    {
        var workflow = Workflow.Create(request.Name, request.Description);

        await _workflowRepository.AddAsync(workflow, cancellationToken);

        return workflow.Id.Value;
    }
}
