using Eleraki.HospitalEngine.Application.DTOs;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.HospitalEngine.Application.Queries;

public record GetPatientByIdQuery(Guid Id) : IRequest<PatientDto?>;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto?>
{
    private readonly IHospitalRepository _repository;

    public GetPatientByIdQueryHandler(IHospitalRepository repository)
    {
        _repository = repository;
    }

    public async Task<PatientDto?> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetPatientByIdAsync(PatientId.From(request.Id), cancellationToken);

        if (patient is null)
            return null;

        return new PatientDto
        {
            Id = patient.Id.Value,
            Name = patient.Name.Value,
            Email = patient.Email.Value,
            Phone = patient.Phone.Value,
            DateOfBirth = patient.DateOfBirth,
            EmergencyContact = patient.EmergencyContact,
            InsuranceNumber = patient.InsuranceNumber,
            Status = patient.Status.ToString()
        };
    }
}
