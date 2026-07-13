namespace Eleraki.HospitalEngine.Application.DTOs;

public class PatientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? EmergencyContact { get; set; }
    public string? InsuranceNumber { get; set; }
    public string Status { get; set; } = string.Empty;
}
