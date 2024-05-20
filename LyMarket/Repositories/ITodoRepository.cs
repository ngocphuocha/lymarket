using LyMarket.Models;

namespace LyMarket.Repositories;

public interface ITodoRepository : IGenericRepository<TodoList>
{
    Task<IEnumerable<TodoList>> GetAllTodoList();
}
