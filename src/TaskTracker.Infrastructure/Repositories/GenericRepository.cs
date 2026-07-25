using Microsoft.EntityFrameworkCore;
using TaskTracker.Infrastructure.Data;
using TaskTracker.Core.Interfaces;


namespace TaskTracker.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T: class    
{
    private readonly TaskTrackerDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(TaskTrackerDbContext context) 
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task<T> AddAsync(T taskItem)
    {
        await _dbSet.AddAsync(taskItem);
        await _context.SaveChangesAsync();
        return taskItem;    
    }
    public async Task<List<T>> GetAllAsync() 
    {
        return await _dbSet.ToListAsync();    
    }
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    
    }
    public async Task<T> UpdateAsync(T taskItem)
    {
        _dbSet.Update(taskItem);
        await _context.SaveChangesAsync();
        return taskItem;    
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var taskToDelete = await _dbSet.FindAsync(id);
        if (taskToDelete == null)
        {
            return false;
        }
        _dbSet.Remove(taskToDelete);
        await _context.SaveChangesAsync();
        return true;    
    }   

}
