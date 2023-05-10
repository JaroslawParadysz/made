using Made.Application.Dto;
using Made.Domain;
using Made.Infrastructure;

namespace Made.Application;

public class Service : IService
{
    private readonly IUnitOfWork _unitOfWork;

    public Service(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> AllocateAsync(AllocateDto allocateDto)
    {
        var batch = await _unitOfWork.BatchRepository.GetAsync(allocateDto.SKU);
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

        await _unitOfWork.SaveAsync();

        return orderLine.Id;
    }

    public async Task<Guid> AddBatchAsync(BatchDto dto)
    {
        var existingBatch = await _unitOfWork.BatchRepository.GetAsync(dto.SKU);
        if (existingBatch is not null)
        {
            throw new Exception("Batch with the same SKU already exists");
        }

        var batch = new Batch(Guid.NewGuid(), dto.SKU, dto.Quantity);
        await _unitOfWork.BatchRepository.InsertAsync(batch);
        
        await _unitOfWork.SaveAsync();

        return batch.Reference;
    }

    public async Task<Guid> AddOrderAsync(OrderDto dto)
    {
        var order = new Order()
        {
            Id = dto.Id
        };

        await _unitOfWork.OrderRepository.AddAsync(order);

        await _unitOfWork.SaveAsync();

        return order.Id;
    }
}