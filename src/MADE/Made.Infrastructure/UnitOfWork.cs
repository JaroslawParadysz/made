using Made.Infrastructure.Database;

namespace Made.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;
    private IBatchRepository _batchRepository;
    private IOrderRepository _orderRepository;

    public UnitOfWork(Context context)
    {
        _context = context;
        _batchRepository = null!;
        _orderRepository = null!;
    }

    public IBatchRepository BatchRepository
    {
        get
        {
            if (_batchRepository is null)
            {
                _batchRepository = new BatchRepository(_context);
            }

            return _batchRepository;
        }
    }
    
    public IOrderRepository OrderRepository
    {
        get
        {
            if (_orderRepository is null)
            {
                _orderRepository = new OrderRepository(_context);
            }

            return _orderRepository;
        }
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}