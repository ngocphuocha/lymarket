using LyMarket.Repositories;

namespace LyMarket.Data;

public interface IUnitOfWork
{
    ITodoRepository TodoLists { get; }

    Task<int> CompleteAsync();
}
