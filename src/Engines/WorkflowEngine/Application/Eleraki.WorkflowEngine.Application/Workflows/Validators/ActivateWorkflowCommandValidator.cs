using FluentValidation;
using Eleraki.WorkflowEngine.Application.Workflows.Commands;

namespace Eleraki.WorkflowEngine.Application.Workflows.Validators;

public class ActivateWorkflowCommandValidator : AbstractValidator<ActivateWorkflowCommand>
{
    public ActivateWorkflowCommandValidator()
    {
        RuleFor(x => x.WorkflowId)
            .NotEmpty()
            .WithMessage("Workflow ID is required.");
    }
}
