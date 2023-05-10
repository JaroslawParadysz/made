using Made.Application.Dto;

namespace Made.Application;

public interface IService
{
    Task<Guid> AllocateAsync(AllocateDto allocateDto);
    Task<Guid> AddBatchAsync(BatchDto dto);
    Task<Guid> AddOrderAsync(OrderDto dto);
}