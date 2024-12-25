using Xunit;
using ProjectManagementApp;

public class ManagerTests
{
    [Fact]
    public void Constructor_ShouldSetRoleToManager()
    {
        string username = "testmanager";

        // Confirming that the Manager constructor assigns the Role property correctly.
        var manager = new Manager(username, "password123", "Frank", "Zappa", "franky@orckandroll.com", "09876554321");

        Assert.Equal(UserRole.Manager, manager.Role);
    }

    [Fact]
    public void SetHashSalt_ShouldUpdatePasswordHashAndSalt()
    {
        var manager = new Manager("testmanager", "password123", "Frank", "Zappa", "franky@orckandroll.com", "09876554321");
        string passwordHash = "hashedpassword";
        byte[] passwordSalt = new byte[] { 1, 2, 3, 4, 5 };

        // Verifies that Manager inherits and correctly uses SetHashSalt from User.
        manager.SetHashSalt(passwordHash, passwordSalt);

        Assert.Equal(passwordHash, manager.PasswordHash);
        Assert.Equal(passwordSalt, manager.PasswordSalt);
    }
}