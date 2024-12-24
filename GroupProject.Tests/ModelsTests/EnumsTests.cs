using Xunit;
using ProjectManagementApp;

namespace ProjectManagementApp.Tests
{
    public class EnumsTests
    {
        [Fact]
        public void UserRole_ShouldHaveExpectedValues()
        {
            Assert.Equal(0, (int)UserRole.TeamMember);
            Assert.Equal(1, (int)UserRole.Manager);
            Assert.Equal(2, (int)UserRole.Intern);
            Assert.Equal(3, (int)UserRole.Administrator);
            Assert.Equal(4, (int)UserRole.System);
        }

        [Fact]
        public void ProjectStatus_ShouldHaveExpectedValues()
        {
            Assert.Equal(0, (int)ProjectStatus.Created);
            Assert.Equal(1, (int)ProjectStatus.InProgress);
            Assert.Equal(2, (int)ProjectStatus.Completed);
            Assert.Equal(3, (int)ProjectStatus.OnHold);
        }

        [Fact]
        public void TaskStatus_ShouldHaveExpectedValues()
        {
            Assert.Equal(0, (int)TaskStatus.Backlog);
            Assert.Equal(1, (int)TaskStatus.ToDo);
            Assert.Equal(2, (int)TaskStatus.InProgress);
            Assert.Equal(3, (int)TaskStatus.Completed);
            Assert.Equal(4, (int)TaskStatus.Blocked);
        }

        [Fact]
        public void TaskType_ShouldHaveExpectedValues()
        {
            Assert.Equal(0, (int)TaskType.Epic);
            Assert.Equal(1, (int)TaskType.Idea);
            Assert.Equal(2, (int)TaskType.Bug);
            Assert.Equal(3, (int)TaskType.Feature);
            Assert.Equal(4, (int)TaskType.Improvement);
            Assert.Equal(5, (int)TaskType.Documentation);
        }
    }
}