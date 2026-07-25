using TaskTracker.Core;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Repositories;

public class InMemoryTaskRepository
{
    private readonly List<TaskItem> _taskItems;
    private int _nextId = 1;

    public InMemoryTaskRepository(List<TaskItem> taskItems)
    {
        _taskItems = taskItems;
    
    }
    public Task<TaskItem> AddAsync(TaskItem task)
    {
        task.TaskId = _nextId++;
        _taskItems.Add(task);

        return Task.FromResult(task);    
    }
    public Task<List<TaskItem>> GetAllAsync()
    {
        var result = _taskItems.ToList();

        return Task.FromResult(result);
    
    }
    public Task<TaskItem?> GetByIdAsync(int id)
    {
        var result = _taskItems.FirstOrDefault(t => t.TaskId == id);
        return Task.FromResult(result);    
    }

    public Task<TaskItem?> UpdateTaskAsync(TaskItem task)
    {
        //_taskItems.Add(task);
        var result = _taskItems.FirstOrDefault(t => t.TaskId == task.TaskId);
        return Task.FromResult(result);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var taskToDelete = _taskItems.FirstOrDefault(t =>t.TaskId == id);
        if (taskToDelete == null)
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(_taskItems.Remove(taskToDelete));    
    }
}
