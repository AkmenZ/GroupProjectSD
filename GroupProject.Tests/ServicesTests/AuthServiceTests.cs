using System;
using Moq;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUsersService> _mockUsersService;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockUsersService = new Mock<IUsersService>();
            _mockPasswordService = new Mock<IPasswordService>();
            _authService = new AuthService(_mockPasswordService.Object);
            _authService.SetUsersService(_mockUsersService.Object);
        }

        [Fact]
        public void Login_ValidCredentials_ShouldLoginSuccessfully()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var passwordHash = "hashedPassword";
            var salt = new byte[] { 1, 2, 3 };
            

            var user = new Intern(username, passwordHash, "Alice", "Wonderland", "alice@wonderland.com", "0987654321", "mentorUser");
            user.SetHashSalt(passwordHash, salt);

            _mockUsersService
                .Setup(s => s.GetUserForLogin(username))
                .Returns(user);

            _mockPasswordService
                .Setup(s => s.VerifyPassword(password, passwordHash, salt))
                .Returns(true);

            // Act
            var result = _authService.Login(username, password);

            // Assert
            Assert.True(result);
            Assert.True(_authService.IsUserLoggedIn);
            Assert.Equal(username, _authService.CurrentUsername);
            Assert.Equal(UserRole.Intern, _authService.CurrentUserRole);
        }

        [Fact]
        public void Login_InvalidUsername_ShouldThrowException()
        {
            // Arrange
            var username = "invaliduser";
            var password = "testpassword";

            _mockUsersService
                .Setup(s => s.GetUserForLogin(username))
                .Returns((User)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _authService.Login(username, password));
            Assert.Equal("Login Error: Invalid username or password", exception.Message);
            Assert.False(_authService.IsUserLoggedIn);
        }

        [Fact]
        public void Login_InvalidPassword_ShouldThrowException()
        {
            // Arrange
            var username = "testuser";
            var password = "wrongpassword";
            var passwordHash = "hashedPassword";
            var salt = new byte[] { 1, 2, 3 };

            var user = new Intern(username, passwordHash, "Test", "User", "alice@wonderland.com", "0987654321", "mentorUser");
            user.SetHashSalt(passwordHash, salt);

            _mockUsersService
                .Setup(s => s.GetUserForLogin(username))
                .Returns(user);

            _mockPasswordService
                .Setup(s => s.VerifyPassword(password, passwordHash, salt))
                .Returns(false);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _authService.Login(username, password));
            Assert.Equal("Login Error: Invalid username or password", exception.Message);
            Assert.False(_authService.IsUserLoggedIn);
        }

        [Fact]
        public void Logout_ShouldResetSessionProperties()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var passwordHash = "hashedPassword";
            var salt = new byte[] { 1, 2, 3 };

            var user = new Intern(username, passwordHash, "Test", "User", "alice@wonderland.com", "0987654321", "mentorUser");
            user.SetHashSalt(passwordHash, salt);

            _mockUsersService
                .Setup(s => s.GetUserForLogin(username))
                .Returns(user);

            _mockPasswordService
                .Setup(s => s.VerifyPassword(password, passwordHash, salt))
                .Returns(true);

            _authService.Login(username, password);

            // Act
            _authService.Logout();

            // Assert
            Assert.False(_authService.IsUserLoggedIn);
            Assert.Null(_authService.CurrentUsername);
            Assert.Equal(default(UserRole), _authService.CurrentUserRole);
        }

        [Fact]
        public void SetUsersService_NullService_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _authService.SetUsersService(null));
        }
    }
}
