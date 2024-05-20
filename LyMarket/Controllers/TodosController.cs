using LyMarket.Helpers.Pagination;
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
                ErrorCode = 1000,
                Message = "Failed to get stores",
                Status = RequestStatus.Fail,
                Detail = e.Message
            });
        }
    }
}
