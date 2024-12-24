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
            public TestTask(int nextTaskID, string title, string description, TaskPriority priority, string assignedTo, int projectID)
                : base(nextTaskID, title, description, priority, assignedTo, projectID)
            {
            }
        }

        // tests for UpdateStatus method
        // status should get updated to a new status

        [Fact]
        public void UpdateStatus_ShouldUpdateStatusWhenStatusIsDifferent() // makeing test method names according to what they should result in
        {
            // Arrange (set up the test)
            var task = new TestTask(1, "Sample Task", "Description", TaskPriority.Medium, "none", 1); // initialy created Task object will have ToDo status
            var newStatus = TaskStatus.InProgress; // declare new status

            // Act (calling testable method)
            var result = task.UpdateStatus(newStatus); // call the UpdateStatus method

            // Assert (compare results)
            Assert.True(result); // check if returned true
            Assert.Equal(newStatus, task.Status); // check if expected value is equal to actual value
        }

        // status should not get updated to the same statsu
        [Fact]
        public void UpdateStatus_ShouldNotUpdateStatusWhenStatusIsSame()
        {
            // Arrange
            var task = new TestTask(1, "Sample Task", "Description", TaskPriority.Medium, "none", 1);
            var initialStatus = TaskStatus.ToDo;

            // Act
            var result = task.UpdateStatus(initialStatus); // attempt to update status the the same status

            // Assert
            Assert.False(result); // check if returned false
            Assert.Equal(initialStatus, task.Status);
        }

        // test for StartTask method
        // starting a task should update its status to In Progress
        [Fact]
        public void StartTask_ShouldSetStatusToInProgress()
        {
            // Arange
            var task = new TestTask(1, "Sample Task", "Description", TaskPriority.Medium, "none", 1); // initial task with staus ToDo

            // Act
            var result = task.StartTask("Janis"); // call method

            // Assert
            Assert.True(result);
            Assert.Equal(TaskStatus.InProgress, task.Status); // compare if task status is InProgress
        }

        // test for AssignUser method
        // method should update the assigned user
        [Fact]
        public void AssignUser_ShouldUpdateAssignedUser()
        {
            // Arrange
            var task = new TestTask(1, "Sample Task", "Description", TaskPriority.Medium, "none", 1);

            // Act
            task.AssignUser("Janis");

            // Assert
            Assert.Equal("Janis", task.AssignedTo); // compare if Janis is equal to assignedTo
        }

        // test for AssignToEpic method
        // method should update epic id
        [Fact]
        public void AssignToEpic_ShouldUpdateEpicID()
        {
            // Arrange
            var task = new TestTask(1, "Sample Task", "Description", TaskPriority.Medium, "none", 1);

            // Act
            task.AssignToEpic("123"); // call method, pass in test epic id

            // Assert
            Assert.Equal("123", task.EpicID); // compare if epic id mathes the test value
        }

        // tests for CompleteTask method
        // method should update the task status to completed
        [Fact]
        public void CompleteTask_ShouldSetStatusToCompleted()
        {
            // Arrange
            var task = new TestTask(1, "Sample Task", "Description", TaskPriority.Medium, "Janis", 1);
            task.UpdateStatus(TaskStatus.InProgress); // update the task staus to InProgress

            // Act
            var result = task.CompleteTask("Janis"); // call the method

            // Assert
            Assert.True(result);
            Assert.Equal(TaskStatus.Completed, task.Status); // compare if task staus is equals to Completed
        }

        [Fact]
        public void CompleteTask_ShouldReturnFalseIfNotInProgress()
        {
            // Arrange
            var task = new TestTask(1, "Sample Task", "Description", TaskPriority.Medium, "Janis", 1); // initial task status is ToDo

            // Act
            var result = task.CompleteTask("Janis"); // call the method

            // Assert
            Assert.False(result); // should be false since task can not be completed from status ToDo
            Assert.NotEqual(TaskStatus.Completed, task.Status); // compare if task status is not Completed
        }
    }
}
