namespace Eleraki.DeliveryEngine.Application.DTOs;

public class DeliveryDto
{
    public Guid Id { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public Guid? DriverId { get; set; }
    public Guid? VehicleId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string? Notes { get; set; }
    public decimal TotalAmount { get; set; }
    public List<DeliveryLineDto> Lines { get; set; } = new();
}
