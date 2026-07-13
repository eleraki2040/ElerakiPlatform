using Eleraki.HospitalEngine.Domain;
using Eleraki.HospitalEngine.Domain.Appointments;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;
using MediatR;

namespace Eleraki.HospitalEngine.Application.Commands;

public record CreatePatientCommand(string Name, string Email, string Phone, DateTime DateOfBirth, string? EmergencyContact = null, string? InsuranceNumber = null) : IRequest<Result<Guid>>;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<Guid>>
{
    private readonly IHospitalRepository _repository;

    public CreatePatientCommandHandler(IHospitalRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var name = PersonName.Create(request.Name);
        var email = Email.Create(request.Email);
        var phone = PhoneNumber.Create(request.Phone);

        var patient = Patient.Create(name, email, phone, request.DateOfBirth, request.EmergencyContact, request.InsuranceNumber);

        await _repository.AddPatientAsync(patient, cancellationToken);

        return Result<Guid>.Success(patient.Id.Value);
    }
}
