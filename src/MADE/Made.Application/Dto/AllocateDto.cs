namespace Made.Application.Dto;

public class AllocateDto
{
    public Guid OrderId { get; set; }
    public string SKU { get; set; }
    public uint Quantity { get; set; }
}