namespace Made.Infrastructure;

public interface IUnitOfWork
{
    IBatchRepository BatchRepository { get; }
    IOrderRepository OrderRepository { get; }
    Task SaveAsync();
}