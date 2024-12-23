using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class TasksServiceTests
    {
        private readonly Mock<IRepository<Task>> _mockTaskRepository;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<ILoggerService> _mockLoggerService;
        private readonly TasksService _tasksService;
        private List<Task> _mockTasks;

        public TasksServiceTests()
        {
            _mockTaskRepository = new Mock<IRepository<Task>>();
            _mockAuthService = new Mock<IAuthService>();
            _mockLoggerService = new Mock<ILoggerService>();

            // Initialize mock tasks
            _mockTasks = new List<Task>
            {
                new TaskBug(1, "Fix Bug", "Fix login bug", "User1", 1),
                new TaskEpic(2, "Epic Task", "Epic description", "User2", 1),
                new TaskFeature(3, "Feature Task", "Add feature", "User3", 1)
            };

            // Set up repository to return mock tasks
            _mockTaskRepository.Setup(repo => repo.GetAll()).Returns(_mockTasks);

            // Initialize service
            _tasksService = new TasksService(
                _mockTaskRepository.Object,
                _mockAuthService.Object,
                _mockLoggerService.Object
            );
        }

        [Fact]
        public void AddTask_ValidInput_ShouldAddTask()
        {
            // Arrange
            var taskType = TaskType.Bug;
            var title = "Fix API Bug";
            var description = "Resolve API timeout";
            var assignedTo = "User2";
            var projectID = 1;

            // Act
            var result = _tasksService.AddTask(taskType, title, description, assignedTo, projectID);

            // Assert
            Assert.True(result);
            Assert.Contains(_mockTasks, t => t.Title == title && t.AssignedTo == assignedTo);
            _mockLoggerService.Verify(logger => logger.LogAction("Task", It.IsAny<string>(), $"assigned to Project:{projectID}"), Times.Once);
        }

        [Fact]
        public void AssignTaskToEpic_ValidInput_ShouldAssignTaskToEpic()
        {
            // Arrange
            var taskId = "1.1";
            var epicId = "2.1";

            // Act
            var result = _tasksService.AssignTaskToEpic(taskId, epicId);

            // Assert
            Assert.True(result);
            var task = _mockTasks.First(t => t.TaskID == taskId);
            Assert.Equal(epicId, task.EpicID);
            _mockLoggerService.Verify(logger => logger.LogAction("Task", taskId, $"assigned to Epic:{epicId}"), Times.Once);
        }

        [Fact]
        public void CompleteTask_ValidInput_ShouldCompleteTask()
        {
            // Arrange
            var taskId = "1.1";
            _mockAuthService.Setup(auth => auth.CurrentUsername).Returns("User1");
            var task = _mockTasks.First(t => t.TaskID == taskId);
            task.StartTask("User1"); // Ensure task is started

            // Act
            var result = _tasksService.CompleteTask(taskId);

            // Assert
            Assert.True(result);
            Assert.Equal(TaskStatus.Completed, task.Status);
            _mockLoggerService.Verify(logger => logger.LogAction("Task", taskId, "Completed"), Times.Once);
        }

        [Fact]
        public void StartTask_ValidOwnership_ShouldStartTask()
        {
            // Arrange
            var taskId = "1.1";
            _mockAuthService.Setup(auth => auth.CurrentUsername).Returns("User1");

            // Act
            var result = _tasksService.StartTask(taskId);

            // Assert
            Assert.True(result);
            var task = _mockTasks.First(t => t.TaskID == taskId);
            Assert.Equal(TaskStatus.InProgress, task.Status);
            _mockLoggerService.Verify(logger => logger.LogAction("Task", taskId, "Started"), Times.Once);
        }

        [Fact]
        public void AssignTaskToEpic_InvalidEpicId_ShouldThrowException()
        {
            // Arrange
            var taskId = "1.1";
            var invalidEpicId = "999.1";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _tasksService.AssignTaskToEpic(taskId, invalidEpicId));
            Assert.Contains("Epic with ID 999.1 not found", exception.Message);
        }

        [Fact]
        public void CompleteTask_InvalidStatus_ShouldThrowException()
        {
            // Arrange
            var taskId = "1.1"; // Task with an invalid status for completion
            var task = _mockTasks.First(t => t.TaskID == taskId);
            task.UpdateStatus(TaskStatus.ToDo); // Set to an invalid status (not InProgress)

            // Act
            var exception = Assert.Throws<Exception>(() => _tasksService.CompleteTask(taskId));

            // Assert
            Assert.Contains("Error Complete Task:", exception.Message); // Simplified check
        }



    }
}
