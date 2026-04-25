using Ruby.Thunder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Ruby.Thunder.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

string authority = builder.Configuration["Auth0:Authority"] ??
    throw new ArgumentNullException("Auth0:Authority");

string audience = builder.Configuration["Auth0:Audience"] ??
    throw new ArgumentNullException("Auth0:Audience");

// 1. Tell .NET to use your Controllers
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
   {
         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   })
    .AddJwtBearer(options =>
    {
            options.Authority = authority;
            options.Audience = audience;

   });

   builder.Services.AddAuthorization(options =>
   {
        options.AddPolicy("delete:catalog", policy =>
            policy.RequireAuthenticatedUser().RequireClaim("scope", "delete:catalog"));
   });

builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite("Data Source = ../Registrat.sqlite",
    b => b.MigrationsAssembly("Ruby.Thunder.Api"))
    );
builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(builder => 
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

// 2. Map the URL routes to your Controllers
app.MapControllers();

app.Run();