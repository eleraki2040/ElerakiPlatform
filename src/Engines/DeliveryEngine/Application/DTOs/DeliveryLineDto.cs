namespace Eleraki.DeliveryEngine.Application.DTOs;

public class DeliveryLineDto
{
    public Guid Id { get; set; }
    public string ProductDescription { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}
