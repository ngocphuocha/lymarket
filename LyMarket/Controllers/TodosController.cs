using LyMarket.Helpers.Pagination;
using LyMarket.Services.Internals.TodoServices;
using LyMarket.Services.TodoServices;
using LyMarket.Services.TodoServices.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LyMarket.Controllers;

[Route("api/todos")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly TodoServices _todoServices;
    public TodosController(TodoServices todoServices)
    {
        _todoServices = todoServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos([FromQuery] RequestParameters request)
    {
        try
        {
            var todos = await _todoServices.GetTodos(request);
            return Ok(new ResponsePaginate<TodoResponse>(todos, todos.MetaData, "Successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseError
            {
                Message = "Failed to get stores",
                Status = RequestStatus.Fail,
                Detail = e.Message
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest request)
    {
        try
        {
            var todo = await _todoServices.CreateTodo(request);
            return Ok(new ResponseItem<TodoResponse>
            {
                Data = todo,
                Message = "Successfully",
                Status = RequestStatus.Success
            });
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseError
            {
                Message = "Failed to create new todo",
                Status = RequestStatus.Fail,
                Detail = e.Message
            });
        }
    }
}
