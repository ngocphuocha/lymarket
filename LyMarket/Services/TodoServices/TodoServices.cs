using LyMarket.Data;
using LyMarket.Extensions;
using LyMarket.Helpers.Pagination;
using LyMarket.Services.TodoServices.DTO;

namespace LyMarket.Services.TodoServices;

public class TodoServices
{
    private readonly LyMarketDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public TodoServices(IUnitOfWork unitOfWork, LyMarketDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }


    public async Task<PaginatedList<TodoResponse>> GetTodos(RequestParameters request)
    {
        var query = _unitOfWork.TodoLists.GetQueryAble().OrderBy(todo => todo.CreatedAt);

        var result = await query.ToPaginatedList<TodoResponse>(request.PageNumber, request.PageSize);
        return result;
    }

    public async Task<TodoResponse> CreateTodo(CreateTodoRequest request) => await _unitOfWork.TodoLists.CreateTodoList(request);
}
