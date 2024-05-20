using LyMarket.Common;
using LyMarket.Models;
using LyMarket.Services.TodoServices.DTO;
using Riok.Mapperly.Abstractions;

namespace LyMarket.Mapper;

[Mapper]
public static partial class DtoMapper
{
    public static partial TTarget MapToDto<TTarget>(BaseEntity source);
    public static partial TodoResponse ToTodoResponse(this TodoList todo);
}
