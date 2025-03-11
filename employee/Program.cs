using employee.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register EmployeeDbContext with Dependency Injection
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("testCon")));

// ✅ Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")  // Your Angular URL
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()); // If using authentication
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Enable CORS before Authorization Middleware
app.UseCors("AllowAngular");

// app.UseHttpsRedirection(); // Disable if using HTTP

app.UseAuthorization();
app.MapControllers();
app.Run();
