using LyMarket.Data;
using LyMarket.Models;
using Microsoft.EntityFrameworkCore;

namespace LyMarket.Repositories;

public class TodoListRepository(LyMarketDbContext context, ILogger logger) : GenericRepository<TodoList>(context, logger), ITodoRepository
{
    private readonly LyMarketDbContext _context = context;

    public async Task<IEnumerable<TodoList>> GetAllTodoList()
    {
        try
        {
            return await _context.TodoLists.ToListAsync();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "{Repo} All method error", typeof(TodoListRepository));
            throw;
        }
    }
}
