using Amazon.S3;
using LyMarket.Data;
using LyMarket.Extensions;
using LyMarket.Models;
using Microsoft.OpenApi.Models;
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
                var allowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")?.Split(',');

                if (allowedOrigins is null || allowedOrigins.Length is 0)
                {

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
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "LyMarket API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddDbContext<LyMarketDbContext>();
builder.Services.AddAuthorizationBuilder();
builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<LyMarketDbContext>();

builder.Services.AddAppService();
builder.Services.AddExternalService();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.MapIdentityApi<AppUser>();

app.MapControllers().RequireAuthorization();

await app.UseCustomConfigAsync();

app.MapGet("/", () => "Hello from Ly Market API!");

app.Run();
