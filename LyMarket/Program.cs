using LyMarket.Data;
using LyMarket.Extensions;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LyMarketDbContext>();
builder.Services.AddAppService();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<LyMarketDbContext>();

    if (context.Database.GetPendingMigrations().Any())
    {
        await context.Database.MigrateAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello from your API on AWS EC2!");

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
