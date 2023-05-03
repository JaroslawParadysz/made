using Made.Application.Dto;

namespace Made.Application;

public interface IService
{
    Task<Guid> AllocateAsync(AllocateDto allocateDto);
}