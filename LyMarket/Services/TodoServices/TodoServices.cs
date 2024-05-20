using LyMarket.Data;
using LyMarket.Extensions;
using LyMarket.Helpers.Pagination;
using LyMarket.Models;
using LyMarket.Services.TodoServices.DTO;

namespace LyMarket.Services.TodoServices;

public class TodoServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly LyMarketDbContext _context;

    public TodoServices(IUnitOfWork unitOfWork, LyMarketDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }


    public async Task<PaginatedList<TodoResponse>> GetTodos(RequestParameters request)
    {
        var query  = _unitOfWork.TodoLists.GetQueryAble();

        var result = await query.ToPaginatedList<TodoResponse, TodoList>(request.PageNumber, request.PageSize);
        return result;
    }
}
