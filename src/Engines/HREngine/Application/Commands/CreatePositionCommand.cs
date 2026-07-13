using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.HREngine.Application.Commands;

public record CreatePositionCommand(string Title, string DepartmentId, string? Description = null) : IRequest<Result<Guid>>;

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Result<Guid>>
{
    private readonly IPositionRepository _repository;

    public CreatePositionCommandHandler(IPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = Position.Create(request.Title, request.DepartmentId, request.Description);

        await _repository.AddAsync(position, cancellationToken);

        return Result<Guid>.Success(position.Id.Value);
    }
}
