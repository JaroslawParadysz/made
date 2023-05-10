using Made.Domain;
using Made.Infrastructure.Database;

namespace Made.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly Context _context;

    public OrderRepository(Context context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);
}