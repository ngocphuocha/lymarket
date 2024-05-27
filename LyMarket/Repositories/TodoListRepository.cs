using LyMarket.Data;
using LyMarket.Mapper;
using LyMarket.Models;
using LyMarket.Services.TodoServices.DTO;
using Microsoft.EntityFrameworkCore;

namespace LyMarket.Repositories;

public class TodoListRepository(LyMarketDbContext context, ILogger logger) : GenericRepository<TodoList>(context, logger), ITodoRepository
{
    private readonly LyMarketDbContext _context = context;

    public async Task<TodoResponse> CreateTodoList(CreateTodoRequest request)
    {
        try
        {
            var todo = request.MapCreateTodoRequestToEntity();
            var newTodo = await _context.TodoLists.AddAsync(todo);
            var result = newTodo.Entity.ToTodoResponse();
            return result;
        }
        catch (Exception e)
        {
            Logger.LogError(e, "{Repo} All method error", typeof(TodoListRepository));
            throw;
        }
    }

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
