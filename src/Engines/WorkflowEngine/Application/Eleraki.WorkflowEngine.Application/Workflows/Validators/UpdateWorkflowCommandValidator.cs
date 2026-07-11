using FluentValidation;
using Eleraki.WorkflowEngine.Application.Workflows.Commands;

namespace Eleraki.WorkflowEngine.Application.Workflows.Validators;

public class UpdateWorkflowCommandValidator : AbstractValidator<UpdateWorkflowCommand>
{
    public UpdateWorkflowCommandValidator()
    {
        RuleFor(x => x.WorkflowId)
            .NotEmpty()
            .WithMessage("Workflow ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Workflow name is required and must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Workflow description must not exceed 1000 characters.");
    }
}
