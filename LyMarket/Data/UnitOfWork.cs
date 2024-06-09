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
        TodoLists = new TodoListRepository(_context, logger);
        Products = new ProductRepository(_context, logger);
    }
    public ITodoRepository TodoLists { get; }

    public IProductRepository Products { get; }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
         return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose() => _context.Dispose();
}
