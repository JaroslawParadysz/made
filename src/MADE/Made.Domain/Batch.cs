namespace Made.Domain;

public class Batch
{
    public Guid Reference { get; private set; }
    public string SKU { get; private set; }
    public uint Quantity { get; private set; }
    public DateTime? Eta { get; private set; }

    public Batch()
    {
        AllocatedOrderLines = new List<OrderLine>();
    }

    public Batch(Guid reference, string sku, uint quantity, DateTime? eta = null)
    {
        Reference = reference;
        SKU = sku;
        Quantity = quantity;
        Eta = eta;
    }
    
    public long AvailableQuantity => Quantity - AllocatedOrderLines.Sum(o => o.Quantity);

    public long AllocatedQuantity => AllocatedOrderLines.Sum(o => o.Quantity);
    
    public ICollection<OrderLine> AllocatedOrderLines { get; set; }

    public bool CanAllocate(OrderLine orderLine) =>
        SKU == orderLine.SKU && AvailableQuantity >= orderLine.Quantity && AllocatedOrderLines.All(o => o.OrderId != orderLine.OrderId);

    public bool TryAllocate(OrderLine orderLine)
    {
        if (!CanAllocate(orderLine))
        {
            return false;
        }
        
        AllocatedOrderLines.Add(orderLine);
        return true;
    }

    public void Deallocate(OrderLine orderLine)
    {
        if (AllocatedOrderLines.Any(o => o.OrderId == orderLine.OrderId))
        {
            AllocatedOrderLines.Remove(orderLine);
        }
    }
}