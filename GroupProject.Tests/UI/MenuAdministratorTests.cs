using System;
using System.IO;
using Moq;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class MenuAdministratorTests
    {
        private readonly Mock<IServicesUI> _mockServicesUI;
        private readonly MenuAdministrator _menuAdministrator;

        public MenuAdministratorTests()
        {
            _mockServicesUI = new Mock<IServicesUI>();
            _menuAdministrator = new MenuAdministrator();
        }

        [Fact]
        public void DisplayMenu_ShouldShowAdministratorOptions()
        {
            // Arrange
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            _menuAdministrator.DisplayMenu();

            // Assert
            var output = consoleOutput.ToString();
            Assert.Contains("[1] Create New Team Member", output);
            Assert.Contains("[8] View Logs", output);
            Assert.Contains("[0] Logout", output);
        }

        [Theory]
        [InlineData(1, UserRole.TeamMember)]
        [InlineData(2, UserRole.Intern)]
        [InlineData(3, UserRole.Manager)]
        [InlineData(4, UserRole.Administrator)]
        public void HandleMenuChoice_CreateUserOptions_ShouldCallCreateUser(int choice, UserRole role)
        {
            // Act
            _menuAdministrator.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(ui => ui.CreateUser(role), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ListAllUsers_ShouldCallListAllUsers()
        {
            // Act
            _menuAdministrator.HandleMenuChoice(5, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(ui => ui.ListAllUsers(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_DeleteUser_ShouldCallDeleteUser()
        {
            // Act
            _menuAdministrator.HandleMenuChoice(6, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(ui => ui.DeleteUser(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ChangePassword_ShouldCallChangePassword()
        {
            // Act
            _menuAdministrator.HandleMenuChoice(7, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(ui => ui.ChangePassword(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ViewLogs_ShouldCallViewLogs()
        {
            // Act
            _menuAdministrator.HandleMenuChoice(8, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(ui => ui.ViewLogs(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_Logout_ShouldCallLogout()
        {
            // Act
            _menuAdministrator.HandleMenuChoice(0, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(ui => ui.Logout(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_InvalidChoice_ShouldNotCallAnyServiceMethod()
        {
            // Arrange
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            _menuAdministrator.HandleMenuChoice(99, _mockServicesUI.Object);

            // Assert
            var output = consoleOutput.ToString().Trim();
            Assert.Contains("Invalid choice.", output);
            _mockServicesUI.VerifyNoOtherCalls();
        }
    }
}
