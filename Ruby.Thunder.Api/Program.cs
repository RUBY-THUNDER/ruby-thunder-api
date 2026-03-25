var builder = WebApplication.CreateBuilder(args);

// 1. Tell .NET to use your Controllers
builder.Services.AddControllers();

var app = builder.Build();

// 2. Map the URL routes to your Controllers
app.MapControllers();

app.Run();