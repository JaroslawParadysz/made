namespace Made.Domain;

public class OrderLine
{
    public Guid Id { get; set; }
    public string SKU { get; private set; }
    public uint Quantity { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid? BatchReference { get; private set; }
    

    public OrderLine()
    {
    }

    public OrderLine(string sku, uint quantity, Guid orderId)
    {
        SKU = sku;
        Quantity = quantity;
        OrderId = orderId;
    }
}