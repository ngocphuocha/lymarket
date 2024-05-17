var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
builder.WebHost.UseUrls("http://0.0.0.0:80"); // Listen on all interfaces, port 80
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello from your API on Render!");

app.UseHttpsRedirection();

app.UseAuthorization();
app.Urls.Add("http://0.0.0.0:80");
app.MapControllers();

app.Run();