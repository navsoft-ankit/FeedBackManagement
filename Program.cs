using Microsoft.EntityFrameworkCore;
using Authservice.Data;
using Authservice.Repository;
using Authservice.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IFormService, FormService>();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();

// Database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();