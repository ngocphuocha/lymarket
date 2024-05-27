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
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return affectedRows;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw; // Rethrow the exception to signal failure
        }
    }

    public void Dispose() => _context.Dispose();
}
