using System.Text.Json.Serialization;

namespace TaskTracker.Core.DTOs;

public class CreateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
   

}
