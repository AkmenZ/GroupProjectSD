using System;
using Moq;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class ServicesUITests
    {
        private readonly Mock<IUsersService> _mockUsersService;
        private readonly Mock<IProjectsService> _mockProjectsService;
        private readonly Mock<ITasksService> _mockTasksService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<ILoggerService> _mockLoggerService;
        private readonly ServicesUI _servicesUI;

        public ServicesUITests()
        {
            _mockUsersService = new Mock<IUsersService>();
            _mockProjectsService = new Mock<IProjectsService>();
            _mockTasksService = new Mock<ITasksService>();
            _mockAuthService = new Mock<IAuthService>();
            _mockLoggerService = new Mock<ILoggerService>();

            _servicesUI = new ServicesUI(
                _mockUsersService.Object,
                _mockProjectsService.Object,
                _mockTasksService.Object,
                _mockLoggerService.Object,
                _mockAuthService.Object);
        }

        [Fact]
        public void Login_ValidCredentials_ShouldLoginSuccessfully()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            var mockPasswordService = new Mock<IPasswordService>();

            string username = "testuser";
            string password = "password123";
            string passwordHash = "hashedpassword";
            byte[] passwordSalt = new byte[] { 1, 2, 3 };
            UserRole userRole = UserRole.Manager;
            string firstName = "John";
            string lastName = "Doe";
            string email = "johndoe@mail.com";
            string phone = "123456789";

            var user = new Manager(username, password, firstName, lastName, email, phone);
            user.SetHashSalt(passwordHash, passwordSalt);


            mockUsersService.Setup(service => service.GetUserForLogin(username)).Returns(user);
            mockPasswordService.Setup(service => service.VerifyPassword(password, passwordHash, passwordSalt)).Returns(true);

            var authService = new AuthService(mockPasswordService.Object);
            authService.SetUsersService(mockUsersService.Object);

            // Act
            var result = authService.Login(username, password);

            // Assert
            Assert.True(result);
            Assert.Equal(username, authService.CurrentUsername);
            Assert.Equal(userRole, authService.CurrentUserRole);
            Assert.True(authService.IsUserLoggedIn);
        }



       /* [Fact]
        public void CreateUser_ValidAdministrator_ShouldCallAddUser()
        {
            // Arrange
            _mockUsersService.Setup(service => service.AddUser(It.IsAny<User>(), It.IsAny<string>())).Returns(true);
            InputService.SetMockInputs(new[] { "admin", "password" });

            // Act
            _servicesUI.CreateUser(UserRole.Administrator);

            // Assert
            _mockUsersService.Verify(service => service.AddUser(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
        */

        [Fact]
        public void ListAllTasks_ShouldPrintTasks()
        {
            // Arrange
            var tasks = new List<Task>
            {
                new TaskBug(1, "Fix Bug", "Fix the login bug", TaskPriority.High, "User1", 1, 3, new DateTime(2025, 12, 12), "Step 1, step 2", "Johnny"),
                new TaskFeature(2, "New Feature", "Add new feature", TaskPriority.Low, "User2", 1, 3, new DateTime(2025, 12, 12), "Criteria 1")
            };

            _mockTasksService.Setup(service => service.GetAllTasks()).Returns(tasks);

            using var consoleOutput = new ConsoleOutput();

            // Act
            _servicesUI.ListAllTasks();

            // Assert
            var output = consoleOutput.GetOutput();
            Assert.Contains("Fix Bug", output);
            Assert.Contains("New Feature", output);
        }


       /* [Fact]
        public void DeleteUser_NonExistentUser_ShouldThrowException()
        {
            // Arrange
            _mockUsersService.Setup(service => service.DeleteUser(It.IsAny<string>())).Throws(new Exception("User not found"));
            InputService.SetMockInputs(new[] { "nonexistentuser" });

            // Act & Assert
            Assert.Throws<Exception>(() => _servicesUI.DeleteUser());
        }

        // More tests for edge cases, other methods...
        */
    }
    

    // Example InputService mock for simulating user input
    public static class InputService
    {
        private static Queue<string> _mockInputs = new Queue<string>();

        public static void SetMockInputs(IEnumerable<string> inputs)
        {
            _mockInputs = new Queue<string>(inputs);
        }

        public static string ReadValidString(string prompt)
        {
            Console.WriteLine(prompt);
            return _mockInputs.Dequeue();
        }

        public static string ReadLineMasked() => ReadValidString("");

        public static int ReadValidInt(string prompt)
        {
            Console.WriteLine(prompt);
            return int.Parse(_mockInputs.Dequeue());
        }
    }
}
