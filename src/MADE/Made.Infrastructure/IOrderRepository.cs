using Made.Domain;

namespace Made.Infrastructure;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}