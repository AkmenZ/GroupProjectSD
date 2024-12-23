using System;
using Moq;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class MenuInternTests
    {
        private readonly Mock<IServicesUI> _mockServicesUI;
        private readonly MenuIntern _menuIntern;

        public MenuInternTests()
        {
            _mockServicesUI = new Mock<IServicesUI>();
            _menuIntern = new MenuIntern();
        }

        [Fact]
        public void DisplayMenu_ShouldOutputCorrectOptions()
        {
            // Arrange
            using var consoleOutput = new ConsoleOutput();

            // Act
            _menuIntern.DisplayMenu();

            // Assert
            var output = consoleOutput.GetOutput();
            Assert.Contains("[1] List Assigned Tasks by Project", output.Trim());
            Assert.Contains("[2] Update Task Status", output.Trim());
            Assert.Contains("[3] Start Task", output.Trim());
            Assert.Contains("[4] Complete Task", output.Trim());
            Assert.Contains("[5] Change Password", output.Trim());
            Assert.Contains("[0] Logout", output.Trim());
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice1_ShouldCallListUserTasks()
        {
            // Arrange
            int choice = 1;

            // Act
            _menuIntern.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.ListUserTasks(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice2_ShouldCallUpdateTaskStatus()
        {
            // Arrange
            int choice = 2;

            // Act
            _menuIntern.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.UpdateTaskStatus(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice3_ShouldCallStartTask()
        {
            // Arrange
            int choice = 3;

            // Act
            _menuIntern.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.StartTask(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice4_ShouldCallCompleteTask()
        {
            // Arrange
            int choice = 4;

            // Act
            _menuIntern.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.CompleteTask(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice5_ShouldCallChangePassword()
        {
            // Arrange
            int choice = 5;

            // Act
            _menuIntern.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.ChangePassword(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice0_ShouldCallLogout()
        {
            // Arrange
            int choice = 0;

            // Act
            _menuIntern.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.Logout(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_InvalidChoice_ShouldOutputErrorMessage()
        {
            // Arrange
            int invalidChoice = 99;
            using var consoleOutput = new ConsoleOutput();

            // Act
            _menuIntern.HandleMenuChoice(invalidChoice, _mockServicesUI.Object);

            // Assert
            var output = consoleOutput.GetOutput();
            Assert.Contains("Invalid choice", output);
        }
    }
}
