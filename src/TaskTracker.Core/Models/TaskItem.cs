namespace TaskTracker.Core.Models;

public class TaskItem
{
    public int TaskId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime ModifiedAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; }

}
