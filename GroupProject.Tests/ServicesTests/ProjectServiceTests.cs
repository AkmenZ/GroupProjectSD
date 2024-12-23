using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class ProjectsServiceTests
    {
        private readonly Mock<IRepository<Project>> _mockProjectRepository;
        private readonly Mock<ILoggerService> _mockLoggerService;
        private readonly ProjectsService _projectsService;
        private List<Project> _mockProjects;

        public ProjectsServiceTests()
        {
            // Initialize mock repository and logger service
            _mockProjectRepository = new Mock<IRepository<Project>>();
            _mockLoggerService = new Mock<ILoggerService>();

            // Initialize mock data
            _mockProjects = new List<Project>
            {
                new Project(1, "Project Alpha", "Manager1", "Description1"),
                new Project(2, "Project Beta", "Manager2", "Description2"),
            };

            // Set up repository to return mock data
            _mockProjectRepository.Setup(repo => repo.GetAll()).Returns(_mockProjects);

            // Create the ProjectsService instance
            _projectsService = new ProjectsService(_mockProjectRepository.Object, _mockLoggerService.Object);
        }

        [Fact]
        public void AddProject_ValidInput_ShouldAddProject()
        {
            // Arrange
            var newProjectName = "Project Gamma";
            var manager = "Manager3";
            var description = "Description3";

            // Act
            var result = _projectsService.AddProject(newProjectName, manager, description);

            // Assert
            Assert.True(result);
            Assert.Contains(_mockProjects, p => p.Name == newProjectName && p.Manager == manager);
            _mockLoggerService.Verify(logger => logger.LogAction("Project", "3", "added"), Times.Once);
        }

        [Fact]
        public void AddProject_DuplicateName_ShouldThrowException()
        {
            // Arrange
            var duplicateProjectName = "Project Alpha";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _projectsService.AddProject(duplicateProjectName, "Manager", "Description"));
            Assert.Equal("\n  Error Add Project: Project: Project Alpha already exists.", exception.Message);
        }

        [Fact]
        public void AddTeamMember_ValidInput_ShouldAddMember()
        {
            // Arrange
            var projectID = 1;
            var username = "NewTeamMember";

            // Act
            var result = _projectsService.AddTeamMember(projectID, username);

            // Assert
            Assert.True(result);
            var project = _mockProjects.First(p => p.ProjectID == projectID);
            Assert.Contains(username, project.TeamMembers);
            _mockLoggerService.Verify(logger => logger.LogAction("Project", projectID.ToString(), $"Team Member added: {username}"), Times.Once);
        }

        [Fact]
        public void UpdateStatus_ValidInput_ShouldUpdateProjectStatus()
        {
            // Arrange
            var projectID = 1;
            var newStatus = ProjectStatus.Completed;

            // Act
            var result = _projectsService.UpdateStatus(projectID, newStatus);

            // Assert
            Assert.True(result);
            var project = _mockProjects.First(p => p.ProjectID == projectID);
            Assert.Equal(newStatus, project.Status);
            _mockLoggerService.Verify(logger => logger.LogAction("Project", projectID.ToString(), $"Status Update to: {newStatus}"), Times.Once);
        }

        [Fact]
        public void DeleteProject_ValidInput_ShouldRemoveProject()
        {
            // Arrange
            var projectID = 1;

            // Act
            var result = _projectsService.DeleteProject(projectID);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(_mockProjects, p => p.ProjectID == projectID);
            _mockLoggerService.Verify(logger => logger.LogAction("Project", projectID.ToString(), "Deleted"), Times.Once);
        }

        [Fact]
        public void GetProjectById_ValidID_ShouldReturnProject()
        {
            // Arrange
            var projectID = 1;

            // Act
            var project = _projectsService.GetProjectById(projectID);

            // Assert
            Assert.NotNull(project);
            Assert.Equal(projectID, project.ProjectID);
        }

        [Fact]
        public void GetProjectsByManager_ValidManager_ShouldReturnProjects()
        {
            // Arrange
            var manager = "Manager1";

            // Act
            var projects = _projectsService.GetProjectsByManager(manager);

            // Assert
            Assert.Single(projects);
            Assert.All(projects, p => Assert.Equal(manager, p.Manager));
        }
    }
}
