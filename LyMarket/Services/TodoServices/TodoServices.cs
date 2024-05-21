using LyMarket.Data;
using LyMarket.Extensions;
using LyMarket.Helpers.Pagination;
using LyMarket.Services.TodoServices.DTO;

namespace LyMarket.Services.TodoServices;

public class TodoServices(IUnitOfWork unitOfWork)
{
    public async Task<PaginatedList<TodoResponse>> GetTodos(RequestParameters request)
    {
        var query = unitOfWork.TodoLists.GetQueryAble().OrderBy(todo => todo.CreatedAt);

        var result = await query.ToPaginatedList<TodoResponse>(request.PageNumber, request.PageSize);
        return result;
    }

    public async Task<TodoResponse> CreateTodo(CreateTodoRequest request) => await unitOfWork.TodoLists.CreateTodoList(request);
}
