using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class UsersServiceTests
    {
        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<ILoggerService> _mockLoggerService;
        private readonly UsersService _usersService;
        private List<User> _mockUsers;

        public UsersServiceTests()
        {
            // Initialize mocks
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockPasswordService = new Mock<IPasswordService>();
            _mockLoggerService = new Mock<ILoggerService>();

            // Initialize mock data
            _mockUsers = new List<User>
            {
                new Manager("Manager1", "password", "Earl", "Grey", "earlgrey@mail.com", "098765543"),
                new Intern("Intern1", "password", "FirstName", "LastName", "firsnamelastname@mail.com", "1234567890", "Manager1")
            };

            _mockUserRepository.Setup(repo => repo.GetAll()).Returns(_mockUsers);
            _usersService = new UsersService(
                _mockUserRepository.Object,
                _mockPasswordService.Object,
                _mockLoggerService.Object
            );
        }

        [Fact]
        public void AddUser_ValidUser_ShouldAddUser()
        {
            // Arrange
            var newUser = new Manager("NewManager", "newPassword", "John", "Rambo", "johnrambo@kickass.com", "987755333");
            var passwordSalt = new byte[] { 1, 2, 3, 4 };
            var passwordHash = "hashedPassword";

            _mockPasswordService.Setup(p => p.GenerateSalt()).Returns(passwordSalt);
            _mockPasswordService.Setup(p => p.HashPassword("newPassword", passwordSalt)).Returns(passwordHash);

            // Act
            var result = _usersService.AddUser(newUser, "newPassword");

            // Assert
            Assert.True(result);
            Assert.Contains(_mockUsers, u => u.Username == "NewManager");
            _mockLoggerService.Verify(logger => logger.LogAction("User", "NewManager", "added"), Times.Once);
        }

        [Fact]
        public void AddUser_ExistingUsername_ShouldThrowException()
        {
            // Arrange
            var existingUser = new Manager("Manager1", "password", "John", "Rambo", "johnrambo@kickass.com", "2092309092");

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _usersService.AddUser(existingUser, "password"));
            Assert.Contains("Username: Manager1 already exists.", exception.Message);
        }

        [Fact]
        public void AddUser_InternWithInvalidMentor_ShouldThrowException()
        {
            // Arrange
            var newIntern = new Intern("Intern2", "password", "FirstName", "LastName", "email@mail.com", "0871234567", "InvalidMentor");

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _usersService.AddUser(newIntern, "password"));
            Assert.Contains("Mentor username: InvalidMentor doesnt exist!", exception.Message);
        }

        [Fact]
        public void DeleteUser_ValidUsername_ShouldDeleteUser()
        {
            // Arrange
            var username = "Manager1";

            // Act
            var result = _usersService.DeleteUser(username);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(_mockUsers, u => u.Username == username);
            _mockLoggerService.Verify(logger => logger.LogAction("User", username, "deleted"), Times.Once);
        }

        [Fact]
        public void DeleteUser_InvalidUsername_ShouldThrowException()
        {
            // Arrange
            var invalidUsername = "NonExistentUser";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _usersService.DeleteUser(invalidUsername));
            Assert.Contains("User with username NonExistentUser not found.", exception.Message);
        }

        [Fact]
        public void ChangePassword_ValidUsername_ShouldChangePassword()
        {
            // Arrange
            var username = "Manager1";
            var newPassword = "newPassword";
            var passwordSalt = new byte[] { 1, 2, 3, 4 };
            var passwordHash = "hashedNewPassword";

            _mockPasswordService.Setup(p => p.GenerateSalt()).Returns(passwordSalt);
            _mockPasswordService.Setup(p => p.HashPassword(newPassword, passwordSalt)).Returns(passwordHash);

            // Act
            var result = _usersService.ChangePassword(username, newPassword);

            // Assert
            Assert.True(result);
            var user = _mockUsers.First(u => u.Username == username);
            Assert.Equal(passwordHash, user.PasswordHash);
            Assert.Equal(passwordSalt, user.PasswordSalt);
            _mockLoggerService.Verify(logger => logger.LogAction("User", username, "password change"), Times.Once);
        }

        [Fact]
        public void ChangePassword_InvalidUsername_ShouldThrowException()
        {
            // Arrange
            var invalidUsername = "NonExistentUser";
            var newPassword = "newPassword";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _usersService.ChangePassword(invalidUsername, newPassword));
            Assert.Contains("User with username NonExistentUser not found.", exception.Message);
        }

        [Fact]
        public void GetUserByUsername_ValidUsername_ShouldReturnUser()
        {
            // Arrange
            var username = "Manager1";

            // Act
            var user = _usersService.GetUserByUsername(username);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(username, user.Username);
        }

        [Fact]
        public void GetUserByUsername_InvalidUsername_ShouldThrowException()
        {
            // Arrange
            var invalidUsername = "NonExistentUser";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _usersService.GetUserByUsername(invalidUsername));
            Assert.Contains("User with username NonExistentUser not found.", exception.Message);
        }
    }
}
