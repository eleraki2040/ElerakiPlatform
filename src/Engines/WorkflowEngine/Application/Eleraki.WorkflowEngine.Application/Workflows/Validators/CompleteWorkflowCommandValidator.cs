using FluentValidation;
using Eleraki.WorkflowEngine.Application.Workflows.Commands;

namespace Eleraki.WorkflowEngine.Application.Workflows.Validators;

public class CompleteWorkflowCommandValidator : AbstractValidator<CompleteWorkflowCommand>
{
    public CompleteWorkflowCommandValidator()
    {
        RuleFor(x => x.WorkflowId)
            .NotEmpty()
            .WithMessage("Workflow ID is required.");
    }
}
