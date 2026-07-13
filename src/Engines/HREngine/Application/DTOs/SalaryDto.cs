namespace Eleraki.HREngine.Application.DTOs;

public class SalaryDto
{
    public Guid Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string? PayGrade { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
}
