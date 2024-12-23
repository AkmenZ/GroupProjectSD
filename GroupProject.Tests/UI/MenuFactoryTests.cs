using System;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class MenuFactoryTests
    {
        [Theory]
        [InlineData(UserRole.Administrator, typeof(MenuAdministrator))]
        [InlineData(UserRole.Manager, typeof(MenuManager))]
        [InlineData(UserRole.TeamMember, typeof(MenuTeamMember))]
        [InlineData(UserRole.Intern, typeof(MenuIntern))]
        public void CreateMenu_ValidRole_ShouldReturnCorrectMenuInstance(UserRole role, Type expectedType)
        {
            // Act
            var menu = MenuFactory.CeateMenu(role);

            // Assert
            Assert.NotNull(menu);
            Assert.IsType(expectedType, menu);
        }

        [Fact]
        public void CreateMenu_InvalidRole_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidRole = (UserRole)99;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => MenuFactory.CeateMenu(invalidRole));
            Assert.Equal("Invalid user role", exception.Message);
        }
    }
}