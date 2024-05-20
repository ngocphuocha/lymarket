using System.ComponentModel.DataAnnotations;

namespace LyMarket.Services.TodoServices.DTO;

public class CreateTodoRequest
{
    [Required]
    [JsonPropertyName("title")]
    public string Title { get; set; }
}
