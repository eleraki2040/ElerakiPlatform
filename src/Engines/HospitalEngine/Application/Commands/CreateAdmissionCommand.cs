using Eleraki.HospitalEngine.Domain.Admissions;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.HospitalEngine.Application.Commands;

public record CreateAdmissionCommand(Guid PatientId, Guid? BedId = null, string? Notes = null) : IRequest<Result<Guid>>;

public class CreateAdmissionCommandHandler : IRequestHandler<CreateAdmissionCommand, Result<Guid>>
{
    private readonly IHospitalRepository _repository;

    public CreateAdmissionCommandHandler(IHospitalRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateAdmissionCommand request, CancellationToken cancellationToken)
    {
        var patientId = PatientId.From(request.PatientId);
        var bedId = request.BedId.HasValue ? BedId.From(request.BedId.Value) : null;

        var admission = Admission.Create(patientId, bedId, request.Notes);

        await _repository.AddAdmissionAsync(admission, cancellationToken);

        return Result<Guid>.Success(admission.Id.Value);
    }
}
