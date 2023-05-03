namespace Made.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<OrderLine> Lines { get; set; }
    }
}