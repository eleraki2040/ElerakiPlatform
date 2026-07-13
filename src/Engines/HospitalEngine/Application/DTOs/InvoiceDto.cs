namespace Eleraki.HospitalEngine.Application.DTOs;

public class InvoiceDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public decimal PaidAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
}
