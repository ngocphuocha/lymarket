using LyMarket.Models;
using LyMarket.Services.TodoServices.DTO;

namespace LyMarket.Repositories;

public interface ITodoRepository : IGenericRepository<TodoList>
{
    Task<IEnumerable<TodoList>> GetAllTodoList();

    Task<TodoResponse> CreateTodoList(CreateTodoRequest request);
}
