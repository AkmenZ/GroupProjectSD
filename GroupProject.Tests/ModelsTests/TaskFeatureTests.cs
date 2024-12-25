using Xunit;
using ProjectManagementApp;

namespace ProjectManagementApp.Tests
{
    public class TaskFeatureTests
    {
        [Fact]
        public void TaskID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskFeature = new TaskFeature(1, "title", "description", TaskPriority.Medium, "assignedTo", 101, 10, new DateTime(2025, 12, 25), "Satisfied users");
            var expectedValue = "1.101"; // Expected string format

            // Act
            var actualValue = taskFeature.TaskID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Title_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskFeature = new TaskFeature(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Satisfied users");
            var expectedValue = "title";

            // Act
            var actualValue = taskFeature.Title;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Description_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskFeature = new TaskFeature(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Satisfied users");
            var expectedValue = "description";

            // Act
            var actualValue = taskFeature.Description;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void AssignedTo_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskFeature = new TaskFeature(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Satisfied users");
            var expectedValue = "assignedTo";

            // Act
            var actualValue = taskFeature.AssignedTo;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ProjectID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskFeature = new TaskFeature(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Satisfied users");
            var expectedValue = 1;

            // Act
            var actualValue = taskFeature.ProjectID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Type_ShouldBeFeature()
        {
            // Arrange
            var taskFeature = new TaskFeature(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 10, new DateTime(2025, 12, 25), "Satisfied users");

            // Act
            var actualValue = taskFeature.Type;

            // Assert
            Assert.Equal(TaskType.Feature, actualValue);
        }
    }
}