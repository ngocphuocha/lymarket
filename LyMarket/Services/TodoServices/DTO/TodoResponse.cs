namespace LyMarket.Services.TodoServices.DTO;

public class TodoResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
}
