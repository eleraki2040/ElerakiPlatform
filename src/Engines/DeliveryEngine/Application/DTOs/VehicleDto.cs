namespace Eleraki.DeliveryEngine.Application.DTOs;

public class VehicleDto
{
    public Guid Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Status { get; set; } = string.Empty;
}
