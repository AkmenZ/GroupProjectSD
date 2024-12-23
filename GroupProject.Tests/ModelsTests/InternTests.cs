using Xunit;
using ProjectManagementApp;

namespace ProjectManagementApp.Tests
{
    public class InternTests
    {
        [Fact]
        public void FirstName_ShouldHaveExpectedValue()
        {
            // Arrange
            var intern = new Intern();
            var expectedValue = "John";

            // Act
            intern.GetType().GetProperty("FirstName").SetValue(intern, expectedValue);

            // Assert
            Assert.Equal(expectedValue, intern.FirstName);
        }

        [Fact]
        public void LastName_ShouldHaveExpectedValue()
        {
            // Arrange
            var intern = new Intern();
            var expectedValue = "Doe";

            // Act
            intern.GetType().GetProperty("LastName").SetValue(intern, expectedValue);

            // Assert
            Assert.Equal(expectedValue, intern.LastName);
        }

        [Fact]
        public void MentorUsername_ShouldHaveExpectedValue()
        {
            // Arrange
            var intern = new Intern();
            var expectedValue = "mentor123";

            // Act
            intern.GetType().GetProperty("MentorUsername").SetValue(intern, expectedValue);

            // Assert
            Assert.Equal(expectedValue, intern.MentorUsername);
        }
    }
}