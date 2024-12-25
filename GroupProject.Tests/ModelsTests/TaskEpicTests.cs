using Xunit;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Tests
{
    public class TaskEpicTests
    {
        [Fact]
        public void TaskID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskEpic = new TaskEpic(1, "title", "description", TaskPriority.Medium, "assignedTo", 101, 15, new DateTime(2025, 12, 24));
            var expectedValue = "1.101"; // Expected string format

            // Act
            var actualValue = taskEpic.TaskID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Title_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskEpic = new TaskEpic(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 24));
            var expectedValue = "title";

            // Act
            var actualValue = taskEpic.Title;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Description_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskEpic = new TaskEpic(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 24));
            var expectedValue = "description";

            // Act
            var actualValue = taskEpic.Description;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void AssignedTo_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskEpic = new TaskEpic(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 24));
            var expectedValue = "assignedTo";

            // Act
            var actualValue = taskEpic.AssignedTo;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ProjectID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskEpic = new TaskEpic(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 24));
            var expectedValue = 1;

            // Act
            var actualValue = taskEpic.ProjectID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Type_ShouldBeEpic()
        {
            // Arrange
            var taskEpic = new TaskEpic(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 24));

            // Act
            var actualValue = taskEpic.Type;

            // Assert
            Assert.Equal(TaskType.Epic, actualValue);
        }
    }
}