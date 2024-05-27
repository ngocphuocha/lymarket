using LyMarket.Repositories;

namespace LyMarket.Data;

public interface IUnitOfWork
{
    ITodoRepository TodoLists { get; }

    IProductRepository Products { get; }

    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
