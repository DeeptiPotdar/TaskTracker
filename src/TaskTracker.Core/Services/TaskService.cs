using System.Diagnostics.Metrics;
using TaskTracker.Core.DTOs;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Services;

public class TaskService :ITaskService
{
    private readonly ITaskRepository _repo;
    //private readonly ILogger<TaskService> _logger;

    public TaskService(ITaskRepository repo)
    {
        _repo = repo;
          
    }

    public async Task<TaskItem> CreateTaskAsync(CreateTaskRequest request)
    {
        if (string.IsNullOrEmpty(request.Title))
        {
            throw new ArgumentException("Title can not be empty", nameof(request));
        
        }

        var now = DateTime.UtcNow;
        var taskItem = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            ModifiedAtUtc = now,
            CreatedAtUtc = now
        };

        var newTask = await _repo.AddAsync(taskItem);
        return newTask;    
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        return await _repo.GetAllAsync();    
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        var taskList = await _repo.GetByIdAsync(id);
        return taskList;    
    }

    public async Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskRequest request) 
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("Title is required");
        }

        var updTask = await _repo.GetByIdAsync(id);

        if (updTask == null)
            return null;

        updTask.Description = request.Description;
        updTask.Title = request.Title;
        updTask.IsCompleted = request.IsCompleted;
        updTask.ModifiedAtUtc = DateTime.UtcNow;

        updTask = await _repo.UpdateAsync(updTask);
        return updTask;
    }

    public async Task<bool> DeleteTaskAsync(int id) 
    {
        var deleteTask = await _repo.DeleteAsync(id);
        return deleteTask;           
    }

}
