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
}