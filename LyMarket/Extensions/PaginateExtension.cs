using LyMarket.Helpers.Pagination;
using LyMarket.Mapper;
using Microsoft.EntityFrameworkCore;

namespace LyMarket.Extensions;

public static class PaginateExtension
{
    public static async Task<PaginatedList<TTarget>> ToPaginatedList<TTarget, TSource>(this IQueryable<TSource> query, int pageNumber, int pageSize)
    {
        var count = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(e => ModelMapper.Map<TTarget, TSource>(e)).ToListAsync();
        return new PaginatedList<TTarget>(items, count, pageNumber, pageSize);
    }
}
