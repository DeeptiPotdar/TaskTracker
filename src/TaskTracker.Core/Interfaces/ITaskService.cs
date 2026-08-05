using TaskTracker.Core.DTOs;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces;

public interface ITaskService
{
    public Task<TaskItem> CreateTaskAsync(CreateTaskRequest request);

    public Task<List<TaskItem>> GetAllTasksAsync();

    public Task<TaskItem?> GetTaskByIdAsync(int id);

    public Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskRequest request);

    public Task<bool> DeleteTaskAsync(int id);

    public Task<List<TaskItem>> GetPendingTasksAsync();

}
