
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces;

public interface ITaskRepository : IGenericRepository<TaskItem> 
{
    Task<List<TaskItem>> GetPendingTasksAsync();
    
}
