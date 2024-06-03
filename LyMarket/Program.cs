using Amazon.S3;
using LyMarket.Data;
using LyMarket.Extensions;
using Microsoft.EntityFrameworkCore;
const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;


builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
        policy =>
        {
            if (environment.IsDevelopment())
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }
            else
            {
                // Get origins from environment variable
                var allowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")?.Split(',');

                if (allowedOrigins is null  || allowedOrigins.Length is 0)
                {
                    // Handle missing or empty environment variable
                    // You might log a warning, throw an exception, or provide a default value
                    throw new Exception("ALLOWED_ORIGINS environment variable is not set or empty.");
                }

                policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }
        });
});

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

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello from Ly Market API!");

app.Run();
