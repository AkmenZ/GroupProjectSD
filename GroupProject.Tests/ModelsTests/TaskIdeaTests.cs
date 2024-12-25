using Xunit;
using ProjectManagementApp;

namespace ProjectManagementApp.Tests
{
    public class TaskIdeaTests
    {
        [Fact]
        public void TaskID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskIdea = new TaskIdea(1, "title", "description", TaskPriority.Medium, "assignedTo", 101, 10, new DateTime(2025, 12, 25), "Alice");
            var expectedValue = "1.101"; // Expected string format

            // Act
            var actualValue = taskIdea.TaskID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Title_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskIdea = new TaskIdea(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Alice");
            var expectedValue = "title";

            // Act
            var actualValue = taskIdea.Title;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Description_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskIdea = new TaskIdea(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Alice");
            var expectedValue = "description";

            // Act
            var actualValue = taskIdea.Description;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void AssignedTo_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskIdea = new TaskIdea(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Alice");
            var expectedValue = "assignedTo";

            // Act
            var actualValue = taskIdea.AssignedTo;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ProjectID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskIdea = new TaskIdea(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Alice");
            var expectedValue = 1;

            // Act
            var actualValue = taskIdea.ProjectID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Type_ShouldBeIdea()
        {
            // Arrange
            var taskIdea = new TaskIdea(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Alice");

            // Act
            var actualValue = taskIdea.Type;

            // Assert
            Assert.Equal(TaskType.Idea, actualValue);
        }
    }
}