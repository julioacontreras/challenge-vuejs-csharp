
// Create the builder
using Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add PostgreSQL Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

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
