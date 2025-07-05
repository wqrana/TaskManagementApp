using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TaskManagementApi.Controllers;
using TaskManagementApi.Models;
using TaskManagementApi.Repository;

namespace TaskManagementApi.Tests.Controllers
{
    [TestFixture]
    public class TasksControllerTests
    {
        private Mock<ITaskRepository> _mockTaskRepository;
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _controller = new TasksController(_mockTaskRepository.Object);
        }

        [Test]
        public async Task GetTasks_ReturnsAllTasks()
        {
            // Arrange
            var testTasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Task 1" },
                new TaskItem { Id = 2, Title = "Task 2" }
            };

            _mockTaskRepository.Setup(repo => repo.GetAllTasksAsync())
                .ReturnsAsync(testTasks);

            // Act
            var result = await _controller.GetTasks();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            var returnedTasks = okResult.Value as IList<TaskItem>;
            Assert.That(returnedTasks, Is.Not.Null);
            Assert.That(returnedTasks?.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetTask_WithValidId_ReturnsTask()
        {
            // Arrange
            var testTask = new TaskItem { Id = 1, Title = "Test Task" };
            _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(1))
                .ReturnsAsync(testTask);

            // Act
            var result = await _controller.GetTask(1);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            var returnTask = okResult.Value as TaskItem;
            Assert.That(returnTask.Id, Is.EqualTo(testTask.Id));
        }

        [Test]
        public async Task GetTask_WithInvalidId_ReturnsNotFound()
        {
            // Arrange           
            _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(It.IsAny<int>()));
            // Act
            var result = await _controller.GetTask(999);

            // Assert           
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
          

        [Test]
        public async Task PutTask_ValidIdAndTask_ReturnsOk()
        {
            // Arrange
            var existingTask = new TaskItem { Id = 1, Title = "Existing Task" };
            var updatedTask = new TaskItem { Id = 1, Title = "Updated Task" };

            _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(1))
                .ReturnsAsync(existingTask);
            _mockTaskRepository.Setup(repo => repo.UpdateTaskAsync(updatedTask))
                .Returns(Task.FromResult(updatedTask));

            // Act
            var result = await _controller.PutTask(1, updatedTask);

            // Assert           
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task PutTask_IdsMismatch_ReturnsBadRequest()
        {
            // Arrange
            var task = new TaskItem { Id = 1, Title = "Test Task" };

            // Act
            var result = await _controller.PutTask(2, task);

            // Assert            
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task PutTask_NonExistentTask_ReturnsNotFound()
        {
            // Arrange
            var task = new TaskItem { Id = 999, Title = "Non-existent Task" };
            _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(It.IsAny<int>()));

            // Act
            var result = await _controller.PutTask(999, task);

            // Assert            
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task ToggleTask_ValidId_TogglesCompletionStatus()
        {
            // Arrange
            var task = new TaskItem { Id = 1, Title = "Test Task", IsCompleted = false };
            _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(1))
                .ReturnsAsync(task);           
            _mockTaskRepository.Setup(repo => repo.UpdateTaskAsync(It.IsAny<TaskItem>()))
                .Returns(Task.FromResult<TaskManagementApi.Models.TaskItem>(null));
            // Act - first toggle
            var result1 = await _controller.ToggleTask(1);

            // Assert - should be true now           
            Assert.That(result1, Is.InstanceOf<OkObjectResult>());          
            Assert.That(task.IsCompleted, Is.EqualTo(true));

            // Act - second toggle
            var result2 = await _controller.ToggleTask(1);

            // Assert - should be false again
            Assert.That(result2, Is.InstanceOf<OkObjectResult>());
            Assert.That(task.IsCompleted, Is.EqualTo(false));
        }

        [Test]
        public async Task ToggleTask_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(999));
               

            // Act
            var result = await _controller.ToggleTask(999);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task DeleteTask_ValidId_ReturnsOk()
        {
            // Arrange
            var deleteTask = new TaskItem { Id = 1, Title = "Test Task" };
            _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(1))
                .ReturnsAsync(deleteTask);

            // Act
            var result = await _controller.DeleteTask(1);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>()) ;
        }
    }
}