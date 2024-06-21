using LyMarket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LyMarket.Data;

public class LyMarketDbContext : IdentityDbContext<AppUser>
{
    private readonly IConfiguration _configuration;
    public LyMarketDbContext(DbContextOptions<LyMarketDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("LyMarketDatabase"));

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            options.LogTo(Console.WriteLine, LogLevel.Information);
            options.EnableSensitiveDataLogging();
        }
    }
}
