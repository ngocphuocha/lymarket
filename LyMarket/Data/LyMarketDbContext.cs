using LyMarket.Models;
using Microsoft.EntityFrameworkCore;

namespace LyMarket.Data;

public class LyMarketDbContext(IConfiguration configuration) : DbContext
{

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        options.UseNpgsql(configuration.GetConnectionString("LyMarketDatabase")); //change to SI
        if (configuration["ENV"] == "DEV") options.LogTo(Console.WriteLine, LogLevel.Information);
    }
}
