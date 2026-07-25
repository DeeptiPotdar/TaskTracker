using Microsoft.EntityFrameworkCore;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Services;
using TaskTracker.Infrastructure.Data;
using TaskTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("TaskTrackerDB");

builder.Services.AddDbContext<TaskTrackerDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

//DI
builder.Services.AddScoped<ITaskRepository,  EfTaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
