using Microsoft.EntityFrameworkCore;

namespace LyMarket.Helpers;

public static class Migration
{
    public static async Task ApplyMigrationAsync<TDbContext>(IServiceScope scope) where TDbContext : DbContext
    {
        await using var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }
    }
    public static void ApplyMigration<T>(IServiceScope scope) => throw new NotImplementedException();
}
