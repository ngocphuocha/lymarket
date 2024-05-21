using LyMarket.Data;
using LyMarket.Services.ProductServices;
using LyMarket.Services.TodoServices;

namespace LyMarket.Extensions;

public static class AppServiceExtension
{
    public static void AddAppService(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<TodoServices>();
        services.AddScoped<ProductServices>();
    }
}
