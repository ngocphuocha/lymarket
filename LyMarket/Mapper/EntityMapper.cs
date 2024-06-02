using LyMarket.Models;
using LyMarket.Services.Internals.ProductServices.Dto;
using LyMarket.Services.TodoServices.DTO;
using Riok.Mapperly.Abstractions;

namespace LyMarket.Mapper;

[Mapper]
public static partial class EntityMapper
{
    public static partial TodoList MapCreateTodoRequestToEntity(this CreateTodoRequest request);
    public static partial Product MapCreateProductRequestToEntity(this CreateProductRequest request);
}
