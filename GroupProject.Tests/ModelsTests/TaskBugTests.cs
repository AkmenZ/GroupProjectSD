using Xunit;
using ProjectManagementApp;

namespace ProjectManagementApp.Tests
{
    public class TaskBugTests
    {
        [Fact]
        public void TaskID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskBug = new TaskBug(1, "title", "description", TaskPriority.Medium, "assignedTo", 101, 3, new DateTime(2025, 12, 12), "Step 1, step 2", "Johnny");
            var expectedValue = "1.101"; // Expected string format

            // Act
            var actualValue = taskBug.TaskID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }


        [Fact]
        public void Title_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskBug = new TaskBug(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 3, new DateTime(2025, 12, 12), "Step 1, step 2", "Johnny");
            var expectedValue = "title";

            // Act
            var actualValue = taskBug.Title;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Description_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskBug = new TaskBug(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 3, new DateTime(2025, 12, 12), "Step 1, step 2", "Johnny");
            var expectedValue = "description";

            // Act
            var actualValue = taskBug.Description;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void AssignedTo_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskBug = new TaskBug(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 3, new DateTime(2025, 12, 12), "Step 1, step 2", "Johnny");
            var expectedValue = "assignedTo";

            // Act
            var actualValue = taskBug.AssignedTo;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ProjectID_ShouldHaveExpectedValue()
        {
            // Arrange
            var taskBug = new TaskBug(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 3, new DateTime(2025, 12, 12), "Step 1, step 2", "Johnny");
            var expectedValue = 1;

            // Act
            var actualValue = taskBug.ProjectID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Type_ShouldBeBug()
        {
            // Arrange
            var taskBug = new TaskBug(1, "title", "description", TaskPriority.Medium, "assignedTo", 1, 3, new DateTime(2025, 12, 12), "Step 1, step 2", "Johnny");

            // Act
            var actualValue = taskBug.Type;

            // Assert
            Assert.Equal(TaskType.Bug, actualValue);
        }
    }
}