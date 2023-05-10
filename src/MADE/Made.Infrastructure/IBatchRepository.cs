using Made.Domain;

namespace Made.Infrastructure;

public interface IBatchRepository
{
    Task<Batch> GetAsync(string sku);
    Task<Batch> GetAsync(Guid reference);
    Task InsertAsync(Batch batch);

}