using Made.Application.Dto;
using Made.Domain;
using Made.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Made.Application;

public class Service : IService
{
    private readonly Context _context;

    public Service(Context context)
    {
        _context = context;
    }

    public async Task<Guid> AllocateAsync(AllocateDto allocateDto)
    {
        var batch = await _context.Batches.SingleOrDefaultAsync(b => b.SKU == allocateDto.SKU);
        if (batch is null)
        {
            throw new Exception("Batch not found");
        }

        var orderLine = new OrderLine(allocateDto.SKU, allocateDto.Quantity, allocateDto.OrderId);
        
        var isAllocated = batch.TryAllocate(orderLine);

        if (!isAllocated)
        {
            throw new Exception("Cannot be allocated");
        }

        await _context.SaveChangesAsync();

        return orderLine.Id;
    }

    public async Task<Guid> AddBatchAsync(BatchDto dto)
    {
        var existingBatch = await _context.Batches.SingleOrDefaultAsync(b => b.SKU == dto.SKU);
        if (existingBatch is not null)
        {
            throw new Exception("Batch with the same SKU already exists");
        }

        var batch = new Batch(Guid.NewGuid(), dto.SKU, dto.Quantity);
        await _context.Batches.AddAsync(batch);
        
        await _context.SaveChangesAsync();

        return batch.Reference;
    }
}