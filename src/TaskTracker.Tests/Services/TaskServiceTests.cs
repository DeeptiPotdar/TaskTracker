using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskTracker.Core.DTOs;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;
using TaskTracker.Core.Services;

namespace TaskTracker.Tests.Services;

[TestClass]
public class TaskServiceTests
{
    [TestMethod]
    public async Task CreateTaskAsync_ValidRequest_ReturnCreatedTask()
    {
        //arrange
        var fakeRepo = new FakeRepo();
        var service = new TaskService(fakeRepo);
        //act

        var request = new CreateTaskRequest
        {
            Title = "Test Task",
            Description = "This is a new Task"
        };

        var result = await service.CreateTaskAsync(request);
        //assert
        Assert.IsNotNull(result);
        Assert.AreEqual(result.Title, request.Title);
        Assert.AreEqual(result.Description, request.Description);
        Assert.IsFalse(result.IsCompleted);
        Assert.AreNotEqual(result.CreatedAtUtc, default);
        Assert.AreNotEqual(result.ModifiedAtUtc, default);
    }

    [TestMethod]
    public async Task CreateTaskAsync_EmptyTitle_ThrowsArgumentException()
    {
        //Arrange
        var fakeRepo = new FakeRepo();
        var service = new TaskService(fakeRepo);
        var request = new CreateTaskRequest
        {
            Title = "",
            Description = "Test empty title"
        };

        //Act  and Assert
        await Assert.ThrowsExceptionAsync<ArgumentException>(
            async () => await service.CreateTaskAsync(request)
            );
    }

    [TestMethod]
    public async Task GetTaskByIdAsync_ExistingId_ReturnsTask()
    {
        //Arrange
        var repo = new FakeRepo();
        var service = new TaskService(repo);
        var task = new TaskItem
        {
            Title = "Seeded task",
            Description = "Already exists",
            IsCompleted = false,
            CreatedAtUtc = DateTime.UtcNow,
            ModifiedAtUtc = DateTime.UtcNow
        };
        var createdTask = await repo.AddAsync(task);

        //Act
        var result = await service.GetTaskByIdAsync(createdTask.TaskId);

        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(createdTask.TaskId, result.TaskId);
        Assert.AreEqual("Seeded task", result.Title);
    }

    public class FakeRepo: ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public Task<TaskItem> AddAsync(TaskItem task)
        {
            task.TaskId = _nextId++;
            _tasks.Add(task);

            return Task.FromResult(task);
        }
        public Task<List<TaskItem>> GetAllAsync()
        {

            return Task.FromResult(_tasks);

        }
        public Task<TaskItem?> GetByIdAsync(int id)
        {
            var result = _tasks.FirstOrDefault(t => t.TaskId == id);
            return Task.FromResult(result);
        }

        public Task<TaskItem?> UpdateAsync(TaskItem task)
        {
            //_taskItems.Add(task);
            var result = _tasks.FirstOrDefault(t => t.TaskId == task.TaskId);
            return Task.FromResult(result);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var taskToDelete = _tasks.FirstOrDefault(t => t.TaskId == id);
            if (taskToDelete == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(_tasks.Remove(taskToDelete));
        }
    }
}
