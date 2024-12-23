using System;
using Moq;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class MenuTeamMemberTests
    {
        private readonly Mock<IServicesUI> _mockServicesUI;
        private readonly MenuTeamMember _menuTeamMember;

        public MenuTeamMemberTests()
        {
            _mockServicesUI = new Mock<IServicesUI>();
            _menuTeamMember = new MenuTeamMember();
        }

        [Fact]
        public void DisplayMenu_ShouldOutputCorrectOptions()
        {
            // Arrange
            using var consoleOutput = new ConsoleOutput();

            // Act
            _menuTeamMember.DisplayMenu();

            // Assert
            var output = consoleOutput.GetOutput();
            Assert.Contains("[1] List Assigned Tasks by Project", output);
            Assert.Contains("[2] List All Tasks", output);
            Assert.Contains("[3] Update Task Status", output);
            Assert.Contains("[4] Start Task", output);
            Assert.Contains("[5] Complete Task", output);
            Assert.Contains("[6] Change Password", output);
            Assert.Contains("[0] Logout", output);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice1_ShouldCallListUserTasks()
        {
            // Arrange
            int choice = 1;

            // Act
            _menuTeamMember.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.ListUserTasks(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice2_ShouldCallListAllTasks()
        {
            // Arrange
            int choice = 2;

            // Act
            _menuTeamMember.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.ListAllTasks(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice3_ShouldCallUpdateTaskStatus()
        {
            // Arrange
            int choice = 3;

            // Act
            _menuTeamMember.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.UpdateTaskStatus(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice4_ShouldCallStartTask()
        {
            // Arrange
            int choice = 4;

            // Act
            _menuTeamMember.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.StartTask(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice5_ShouldCallCompleteTask()
        {
            // Arrange
            int choice = 5;

            // Act
            _menuTeamMember.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.CompleteTask(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice6_ShouldCallChangePassword()
        {
            // Arrange
            int choice = 6;

            // Act
            _menuTeamMember.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            _mockServicesUI.Verify(s => s.ChangePassword(), Times.Once);
        }

        [Fact]
        public void HandleMenuChoice_ValidChoice0_ShouldCallLogout()
        {
            // Arrange
            int choice = 0;

            // Act
            _menuTeamMember.HandleMenuChoice(choice, _mockServicesUI.Object);

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
            _menuTeamMember.HandleMenuChoice(invalidChoice, _mockServicesUI.Object);

            // Assert
            var output = consoleOutput.GetOutput();
            Assert.Contains("Invalid choice", output);
        }
    }
    
}
