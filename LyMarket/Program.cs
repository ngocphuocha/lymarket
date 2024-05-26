using Amazon.S3;
using LyMarket.Data;
using LyMarket.Extensions;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppService(); // Internal services like repository, unit of work, ....
builder.Services.AddExternalService(); // External service like database, aws, azure, cloud, infrastructure

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

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

app.MapGet("/", () => "Hello from Ly Market API!");

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
