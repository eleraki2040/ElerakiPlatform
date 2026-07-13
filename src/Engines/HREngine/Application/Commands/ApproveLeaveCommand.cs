using Eleraki.HREngine.Domain;
using Eleraki.HREngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.HREngine.Application.Commands;

public record ApproveLeaveCommand(Guid LeaveId, string ApprovedBy) : IRequest<Result<Unit>>;

public class ApproveLeaveCommandHandler : IRequestHandler<ApproveLeaveCommand, Result<Unit>>
{
    private readonly ILeaveRepository _leaveRepository;

    public ApproveLeaveCommandHandler(ILeaveRepository leaveRepository)
    {
        _leaveRepository = leaveRepository;
    }

    public async Task<Result<Unit>> Handle(ApproveLeaveCommand request, CancellationToken cancellationToken)
    {
        var leave = await _leaveRepository.GetByIdAsync(LeaveId.From(request.LeaveId), cancellationToken);
        if (leave is null)
            return Result<Unit>.Failure(Error.NotFound("Leave request not found."));

        leave.Approve(request.ApprovedBy);

        await _leaveRepository.UpdateAsync(leave, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
