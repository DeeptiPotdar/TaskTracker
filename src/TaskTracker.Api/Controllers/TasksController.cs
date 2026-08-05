using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Core;
using TaskTracker.Core.DTOs;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskService service, ILogger<TasksController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request can not be null");
        }

        try
        {
            var newTask = await _service.CreateTaskAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = newTask.TaskId }, newTask);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id is required can not be zero");
        }
        var result = await _service.GetTaskByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllTasksAsync();

        return Ok(result);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id , UpdateTaskRequest updateTask)
    {
        if (id <= 0)
        {
            return BadRequest("Id is required can not be zero");
        }
        if (updateTask == null)
        {
            return BadRequest("Request cannot be null");
        }

        var result = await _service.UpdateTaskAsync(id, updateTask);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);   
    
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id) 
    {
        if (id <= 0)
        {
            return BadRequest("Task Id is required");
        }
        var result = await _service.DeleteTaskAsync(id);
        if (!result)
        {
            return NotFound();        
        }
        return NoContent();    
    }

    [HttpGet("pending")]
    public async Task<ActionResult<List<TaskItem>>> GetPendingTaskAsync() 
    {
        var result = await _service.GetPendingTasksAsync(); 
        return Ok(result);    
    }
}
