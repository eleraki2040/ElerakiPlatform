using FluentValidation;
using Eleraki.WorkflowEngine.Application.Workflows.Commands;

namespace Eleraki.WorkflowEngine.Application.Workflows.Validators;

public class DeleteWorkflowCommandValidator : AbstractValidator<DeleteWorkflowCommand>
{
    public DeleteWorkflowCommandValidator()
    {
        RuleFor(x => x.WorkflowId)
            .NotEmpty()
            .WithMessage("Workflow ID is required.");
    }
}
