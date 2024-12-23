using Xunit;
using Moq;

namespace ProjectManagementApp.Tests
{
    public class MenuManagerTests
    {
        private readonly MenuManager _menuManager;
        private readonly Mock<IServicesUI> _mockServicesUI;

        public MenuManagerTests()
        {
            _menuManager = new MenuManager();
            _mockServicesUI = new Mock<IServicesUI>();
        }

        [Fact]
        public void DisplayMenu_ShouldNotThrowExceptions()
        {
            // Act
            var exception = Record.Exception(() => _menuManager.DisplayMenu());

            // Assert
            Assert.Null(exception); // Ensure no exceptions are thrown
        }

        [Theory]
        [InlineData(1, nameof(IServicesUI.CreateProject))]
        [InlineData(2, nameof(IServicesUI.AddTeamMemberToProject))]
        [InlineData(3, nameof(IServicesUI.UpdateProjectStatus))]
        [InlineData(4, nameof(IServicesUI.DeleteProject))]
        [InlineData(5, nameof(IServicesUI.ListAllProjects))]
        [InlineData(6, nameof(IServicesUI.ListProjectsByUser))]
        [InlineData(7, nameof(IServicesUI.ListProjectsByManager))]
        [InlineData(8, nameof(IServicesUI.ListProjectsByStatus))]
        [InlineData(9, nameof(IServicesUI.ListProjectTeamMembers))]
        [InlineData(10, nameof(IServicesUI.ListAllTasks))]
        [InlineData(11, nameof(IServicesUI.ListProjectTasks))]
        [InlineData(12, nameof(IServicesUI.ListTasksByManager))]
        [InlineData(13, nameof(IServicesUI.ListProjectTasksByStatus))]
        [InlineData(14, nameof(IServicesUI.ListProjectTasksByType))]
        [InlineData(15, nameof(IServicesUI.ListProjectTasksByUser))]
        [InlineData(16, nameof(IServicesUI.ListEpicTasks))]
        [InlineData(17, nameof(IServicesUI.AddTask))]
        [InlineData(18, nameof(IServicesUI.AssignUserToTask))]
        [InlineData(19, nameof(IServicesUI.AssignTaskToEpic))]
        [InlineData(20, nameof(IServicesUI.StartTask))]
        [InlineData(21, nameof(IServicesUI.UpdateTaskStatus))]
        [InlineData(22, nameof(IServicesUI.CompleteTask))]
        [InlineData(23, nameof(IServicesUI.DeleteTask))]
        [InlineData(24, nameof(IServicesUI.ChangePassword))]
        [InlineData(0, nameof(IServicesUI.Logout))]
        public void HandleMenuChoice_ValidChoice_ShouldInvokeCorrectServiceMethod(int choice, string methodName)
        {
            // Act
            _menuManager.HandleMenuChoice(choice, _mockServicesUI.Object);

            // Assert
            switch (methodName)
            {
                case nameof(IServicesUI.CreateProject):
                    _mockServicesUI.Verify(service => service.CreateProject(), Times.Once);
                    break;
                case nameof(IServicesUI.AddTeamMemberToProject):
                    _mockServicesUI.Verify(service => service.AddTeamMemberToProject(), Times.Once);
                    break;
                case nameof(IServicesUI.UpdateProjectStatus):
                    _mockServicesUI.Verify(service => service.UpdateProjectStatus(), Times.Once);
                    break;
                case nameof(IServicesUI.DeleteProject):
                    _mockServicesUI.Verify(service => service.DeleteProject(), Times.Once);
                    break;
                case nameof(IServicesUI.ListAllProjects):
                    _mockServicesUI.Verify(service => service.ListAllProjects(), Times.Once);
                    break;
                case nameof(IServicesUI.ListProjectsByUser):
                    _mockServicesUI.Verify(service => service.ListProjectsByUser(), Times.Once);
                    break;
                case nameof(IServicesUI.ListProjectsByManager):
                    _mockServicesUI.Verify(service => service.ListProjectsByManager(), Times.Once);
                    break;
                case nameof(IServicesUI.ListProjectsByStatus):
                    _mockServicesUI.Verify(service => service.ListProjectsByStatus(), Times.Once);
                    break;
                case nameof(IServicesUI.ListProjectTeamMembers):
                    _mockServicesUI.Verify(service => service.ListProjectTeamMembers(), Times.Once);
                    break;
                case nameof(IServicesUI.ListAllTasks):
                    _mockServicesUI.Verify(service => service.ListAllTasks(), Times.Once);
                    break;
                case nameof(IServicesUI.ListProjectTasks):
                    _mockServicesUI.Verify(service => service.ListProjectTasks(), Times.Once);
                    break;
                case nameof(IServicesUI.ListTasksByManager):
                    _mockServicesUI.Verify(service => service.ListTasksByManager(), Times.Once);
                    break;
                case nameof(IServicesUI.ListProjectTasksByStatus):
                    _mockServicesUI.Verify(service => service.ListProjectTasksByStatus(), Times.Once);
                    break;
                case nameof(IServicesUI.ListProjectTasksByType):
                    _mockServicesUI.Verify(service => service.ListProjectTasksByType(), Times.Once);
                    break;
                case nameof(IServicesUI.ListProjectTasksByUser):
                    _mockServicesUI.Verify(service => service.ListProjectTasksByUser(), Times.Once);
                    break;
                case nameof(IServicesUI.ListEpicTasks):
                    _mockServicesUI.Verify(service => service.ListEpicTasks(), Times.Once);
                    break;
                case nameof(IServicesUI.AddTask):
                    _mockServicesUI.Verify(service => service.AddTask(), Times.Once);
                    break;
                case nameof(IServicesUI.AssignUserToTask):
                    _mockServicesUI.Verify(service => service.AssignUserToTask(), Times.Once);
                    break;
                case nameof(IServicesUI.AssignTaskToEpic):
                    _mockServicesUI.Verify(service => service.AssignTaskToEpic(), Times.Once);
                    break;
                case nameof(IServicesUI.StartTask):
                    _mockServicesUI.Verify(service => service.StartTask(), Times.Once);
                    break;
                case nameof(IServicesUI.UpdateTaskStatus):
                    _mockServicesUI.Verify(service => service.UpdateTaskStatus(), Times.Once);
                    break;
                case nameof(IServicesUI.CompleteTask):
                    _mockServicesUI.Verify(service => service.CompleteTask(), Times.Once);
                    break;
                case nameof(IServicesUI.DeleteTask):
                    _mockServicesUI.Verify(service => service.DeleteTask(), Times.Once);
                    break;
                case nameof(IServicesUI.ChangePassword):
                    _mockServicesUI.Verify(service => service.ChangePassword(), Times.Once);
                    break;
                case nameof(IServicesUI.Logout):
                    _mockServicesUI.Verify(service => service.Logout(), Times.Once);
                    break;
                default:
                    throw new ArgumentException($"Unsupported method: {methodName}");
            }
        }
    }
}
