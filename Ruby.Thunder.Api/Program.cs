using Ruby.Thunder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// 1. Tell .NET to use your Controllers
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite("Data Source = ../Registrat.sqlite",
    b => b.MigrationsAssembly("Ruby.Thunder.Data"))
    );
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. Map the URL routes to your Controllers
app.MapControllers();

app.Run();