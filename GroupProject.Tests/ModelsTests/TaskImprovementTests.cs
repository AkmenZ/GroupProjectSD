using Xunit;
using ProjectManagementApp;

namespace ProjectManagementApp.Tests
{
    public class TaskImprovementTests
    {
        [Fact]
        public void TaskID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskImprovement = new TaskImprovement(1, "title", "description", TaskPriority.Medium, "assignedTo", 101, 5, new DateTime(2025, 12, 25), "Feature #131");
            var expectedValue = "1.101"; // Expected string format

            // Act
            var actualValue = taskImprovement.TaskID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Title_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskImprovement = new TaskImprovement(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 25), "Feature #131");
            var expectedValue = "title";

            // Act
            var actualValue = taskImprovement.Title;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Description_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskImprovement = new TaskImprovement(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 25), "Feature #131");
            var expectedValue = "description";

            // Act
            var actualValue = taskImprovement.Description;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void AssignedTo_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskImprovement = new TaskImprovement(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 25), "Feature #131");
            var expectedValue = "assignedTo";

            // Act
            var actualValue = taskImprovement.AssignedTo;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ProjectID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskImprovement = new TaskImprovement(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 25), "Feature #131");
            var expectedValue = 1;

            // Act
            var actualValue = taskImprovement.ProjectID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Type_ShouldBeImprovement()
        {
            // Arrange
            var taskImprovement = new TaskImprovement(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 15, new DateTime(2025, 12, 25), "Feature #131");

            // Act
            var actualValue = taskImprovement.Type;

            // Assert
            Assert.Equal(TaskType.Improvement, actualValue);
        }
    }
}