using Xunit;
using ProjectManagementApp;
using System;

namespace ProjectManagementApp.Tests
{
    public class LogTests
    {
        [Fact]
        public void LogID_ShouldHaveExpectedValue()
        {
            // Arrange
            var log = new Log(1, "itemId", "item", "action", "username", UserRole.TeamMember);
            var expectedValue = 1;

            // Act
            var actualValue = log.LogID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Time_ShouldHaveExpectedValue()
        {
            // Arrange
            var log = new Log(1, "itemId", "item", "action", "username", UserRole.TeamMember);
            var expectedValue = DateTime.Now;

            // Act
            var actualValue = log.Time;

            // Assert
            Assert.Equal(expectedValue.Date, actualValue.Date);
        }

        [Fact]
        public void Item_ShouldHaveExpectedValue()
        {
            // Arrange
            var log = new Log(1, "itemId", "item", "action", "username", UserRole.TeamMember);
            var expectedValue = "item";

            // Act
            var actualValue = log.Item;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ItemID_ShouldHaveExpectedValue()
        {
            // Arrange
            var log = new Log(1, "itemId", "item", "action", "username", UserRole.TeamMember);
            var expectedValue = "itemId";

            // Act
            var actualValue = log.ItemID;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Action_ShouldHaveExpectedValue()
        {
            // Arrange
            var log = new Log(1, "itemId", "item", "action", "username", UserRole.TeamMember);
            var expectedValue = "action";

            // Act
            var actualValue = log.Action;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Username_ShouldHaveExpectedValue()
        {
            // Arrange
            var log = new Log(1, "itemId", "item", "action", "username", UserRole.TeamMember);
            var expectedValue = "username";

            // Act
            var actualValue = log.Username;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Role_ShouldHaveExpectedValue()
        {
            // Arrange
            var log = new Log(1, "itemId", "item", "action", "username", UserRole.TeamMember);
            var expectedValue = UserRole.TeamMember;

            // Act
            var actualValue = log.Role;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}