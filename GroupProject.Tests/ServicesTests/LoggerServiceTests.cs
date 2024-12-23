using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class LoggerServiceTests
    {
        private readonly Mock<IRepository<Log>> _mockLogRepository;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly LoggerService _loggerService;

        public LoggerServiceTests()
        {
            _mockLogRepository = new Mock<IRepository<Log>>();
            _mockAuthService = new Mock<IAuthService>();
            _loggerService = new LoggerService(_mockLogRepository.Object, _mockAuthService.Object);
        }

        [Fact]
        public void LogAction_ValidInput_AddsLog()
        {
            // Arrange
            var logs = new List<Log>();
            _mockLogRepository.Setup(repo => repo.GetAll()).Returns(logs);
            _mockAuthService.Setup(auth => auth.CurrentUsername).Returns("testuser");
            _mockAuthService.Setup(auth => auth.CurrentUserRole).Returns(UserRole.Manager);

            var item = "Task";
            var itemId = "123";
            var action = "Created";

            // Act
            _loggerService.LogAction(item, itemId, action);

            // Assert
            Assert.Single(logs); // Ensure a single log was added
            var log = logs.First();
            Assert.Equal(item, log.Item);
            Assert.Equal(itemId, log.ItemID);
            Assert.Equal(action, log.Action);
            Assert.Equal("testuser", log.Username);
            Assert.Equal(UserRole.Manager, log.Role);
        }

        [Fact]
        public void LogAction_RepositoryThrowsException_CatchesAndLogsError()
        {
            // Arrange
            _mockLogRepository.Setup(repo => repo.GetAll()).Throws(new Exception("Repository error"));

            // Act & Assert
            var exception = Record.Exception(() => _loggerService.LogAction("Task", "123", "Created"));
            Assert.Null(exception); // Ensure the exception is caught and doesn't propagate
        }

        [Fact]
        public void GetLogs_ReturnsLogsFromRepository()
        {
            // Arrange
            var logs = new List<Log>
            {
                new Log(1, "123", "Task", "Created", "testuser", UserRole.Manager)
            };
            _mockLogRepository.Setup(repo => repo.GetAll()).Returns(logs);

            // Act
            var result = _loggerService.GetLogs();

            // Assert
            Assert.Single(result);
            Assert.Equal(logs.First(), result.First());
        }

        [Fact]
        public void GetLogs_RepositoryThrowsException_ThrowsExceptionWithMessage()
        {
            // Arrange
            _mockLogRepository.Setup(repo => repo.GetAll()).Throws(new Exception("Repository error"));

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _loggerService.GetLogs());
            Assert.Equal("Error getting logs: Repository error", exception.Message);
        }
    }

    // Log class already satisfies the ILog interface
}
