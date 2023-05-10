using Made.Domain;
using Made.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Made.Infrastructure;

public class BatchRepository : IBatchRepository
{
    private readonly Context _context;

    public BatchRepository(Context context)
    {
        _context = context;
    }

    public async Task<Batch> GetAsync(string sku)
    {
        return await _context.Batches.SingleOrDefaultAsync(b => b.SKU == sku);
    }

    public async Task<Batch> GetAsync(Guid reference)
    {
        return await _context.Batches.SingleOrDefaultAsync(b => b.Reference == reference);
    }

    public async Task InsertAsync(Batch batch)
    {
        await _context.Batches.AddAsync(batch);
    }
}