namespace TaskTracker.Core.Interfaces;

public interface IGenericRepository<T> where T: class 
{
    Task<T> AddAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}
