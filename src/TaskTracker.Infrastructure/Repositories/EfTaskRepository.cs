using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;
using TaskTracker.Infrastructure.Data;

namespace TaskTracker.Infrastructure.Repositories;

public class EfTaskRepository : GenericRepository<TaskItem>, ITaskRepository
{
    private readonly TaskTrackerDbContext _context;
    private readonly ILogger<EfTaskRepository> _logger;

    public EfTaskRepository(TaskTrackerDbContext context, ILogger<EfTaskRepository> logger) 
        :base(context)
    {
        _context = context;
        _logger = logger;    
    }

    public async Task<List<TaskItem>> GetPendingTasksAsync()
    {
        return await _context.Tasks
            .Where(t => !t.IsCompleted).ToListAsync();    
    }
}
