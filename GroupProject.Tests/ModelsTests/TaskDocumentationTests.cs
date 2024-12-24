using Xunit;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Tests
{
    public class TaskDocumentationTests
    {
        [Fact]
        public void TaskID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskDocumentation = new TaskDocumentation(1, "title", "description", TaskPriority.Medium, "assignedTo", 101);
            var expectedValue = "1.101"; // Expected string format

            // Act
            var actualValue = taskDocumentation.TaskID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Title_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskDocumentation = new TaskDocumentation(1, "title", "description", TaskPriority.Medium, "assignedTo", 1);
            var expectedValue = "title";

            // Act
            var actualValue = taskDocumentation.Title;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Description_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskDocumentation = new TaskDocumentation(1, "title", "description", TaskPriority.Medium, "assignedTo", 1);
            var expectedValue = "description";

            // Act
            var actualValue = taskDocumentation.Description;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void AssignedTo_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskDocumentation = new TaskDocumentation(1, "title", "description", TaskPriority.Medium, "assignedTo", 1);
            var expectedValue = "assignedTo";

            // Act
            var actualValue = taskDocumentation.AssignedTo;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ProjectID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskDocumentation = new TaskDocumentation(1, "title", "description", TaskPriority.Medium, "assignedTo", 1);
            var expectedValue = 1;

            // Act
            var actualValue = taskDocumentation.ProjectID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Type_ShouldBeDocumentation()
        {
            // Arrange
            var taskDocumentation = new TaskDocumentation(1, "title", "description", TaskPriority.Medium, "assignedTo", 1);

            // Act
            var actualValue = taskDocumentation.Type;

            // Assert
            Assert.Equal(TaskType.Documentation, actualValue);
        }
    }
}