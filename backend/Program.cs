// Create the builder
var builder = WebApplication.CreateBuilder(args);

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
