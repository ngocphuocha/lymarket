using LyMarket.Data;
using Microsoft.EntityFrameworkCore;

namespace LyMarket.Extensions;

public static class AppConfig
{
    public static async Task UseCustomConfigAsync(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LyMarketDbContext>();

            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
