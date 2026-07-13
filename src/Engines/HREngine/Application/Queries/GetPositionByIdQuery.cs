using Eleraki.HREngine.Application.DTOs;
using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using MediatR;

namespace Eleraki.HREngine.Application.Queries;

public record GetPositionByIdQuery(Guid Id) : IRequest<PositionDto?>;

public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, PositionDto?>
{
    private readonly IPositionRepository _repository;

    public GetPositionByIdQueryHandler(IPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task<PositionDto?> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
    {
        var position = await _repository.GetByIdAsync(PositionId.From(request.Id), cancellationToken);

        if (position is null)
            return null;

        return new PositionDto
        {
            Id = position.Id.Value,
            Title = position.Title,
            Description = position.Description,
            DepartmentId = position.DepartmentId,
            Status = position.Status.ToString()
        };
    }
}
