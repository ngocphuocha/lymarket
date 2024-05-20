using LyMarket.Repositories;

namespace LyMarket.Data;

public class UnitOfWork : IUnitOfWork
{

    private readonly LyMarketDbContext _context;

    public UnitOfWork(
        LyMarketDbContext context,
        ILoggerFactory loggerFactory
    )
    {
        var logger = loggerFactory.CreateLogger("logs");
        _context = context;
        TodoLists = new TodoListRepository(context, logger);
    }
    public ITodoRepository TodoLists { get; }
    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
