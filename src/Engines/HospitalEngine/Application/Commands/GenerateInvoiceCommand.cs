using Eleraki.HospitalEngine.Domain.Billing;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.HospitalEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;
using MediatR;

namespace Eleraki.HospitalEngine.Application.Commands;

public record GenerateInvoiceCommand(Guid PatientId, decimal TotalAmount, string Currency, DateTime DueDate) : IRequest<Result<Guid>>;

public class GenerateInvoiceCommandHandler : IRequestHandler<GenerateInvoiceCommand, Result<Guid>>
{
    private readonly IHospitalRepository _repository;

    public GenerateInvoiceCommandHandler(IHospitalRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(GenerateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var patientId = PatientId.From(request.PatientId);
        var totalAmount = Money.Create(request.TotalAmount, request.Currency);

        var invoice = Invoice.Create(patientId, totalAmount, request.DueDate);

        await _repository.AddInvoiceAsync(invoice, cancellationToken);

        return Result<Guid>.Success(invoice.Id.Value);
    }
}
