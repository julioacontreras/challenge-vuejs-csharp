
// Create the builder
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);


// Add PostgreSQL Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
    dataSourceBuilder.EnableDynamicJson(); // Enable JSON serialization
    var dataSource = dataSourceBuilder.Build();
    options.UseNpgsql(dataSource);
});

// Add CORS to can have access from the frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Method to registers my controllers
builder.Services.AddControllers();

// Create my application
var app = builder.Build();

// Set route, authorizations and mapping controllers
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Run the application
app.Run();
