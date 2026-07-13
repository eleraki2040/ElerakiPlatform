using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.HREngine.Application.Commands;

public record CreateDepartmentCommand(string Name, string? Description = null) : IRequest<Result<Guid>>;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<Guid>>
{
    private readonly IDepartmentRepository _repository;

    public CreateDepartmentCommandHandler(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = Department.Create(request.Name, request.Description);

        await _repository.AddAsync(department, cancellationToken);

        return Result<Guid>.Success(department.Id.Value);
    }
}
