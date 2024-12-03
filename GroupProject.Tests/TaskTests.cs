using System;
using Xunit;
using ProjectManagementApp;

namespace ProjectManagementApp.Tests
{
    public class TaskTests
    {
        // since Task class is abstract, we need to create a concrete class for it to test
        private class TestTask : Task
        {
            public TestTask(int nextTaskID, string title, string description, string assignedTo, int projectID)
                : base(nextTaskID, title, description, assignedTo, projectID)
            {
            }
        }

        // tests for UpdateStatus method
        // status should get updated to a new status

        [Fact]
        public void UpdateStatus_ShouldUpdateStatusWhenStatusIsDifferent()
        {
            // Arrange
            var task = new TestTask(1, "Sample Task", "Description", "none", 1); // initialy created Task object will have ToDo status
            var newStatus = TaskStatus.InProgress; // declare new status

            // Act
            var result = task.UpdateStatus(newStatus); // call the UpdateStatus method

            // Assert
            Assert.True(result); // check if returned true
            Assert.Equal(newStatus, task.Status); // check if expected value is equal to actual value
        }

        // status should not get updated to the same statsu
        [Fact]
        public void UpdateStatus_ShouldNotUpdateStatusWhenStatusIsSame()
        {
            // Arrange
            var task = new TestTask(1, "Sample Task", "Description", "none", 1);
            var initialStatus = TaskStatus.ToDo;

            // Act
            var result = task.UpdateStatus(initialStatus); // attempt to update status the the same status

            // Assert
            Assert.False(result); // check if returned false
            Assert.Equal(initialStatus, task.Status);
        }

        // othet task related test can go here...
    }
}
