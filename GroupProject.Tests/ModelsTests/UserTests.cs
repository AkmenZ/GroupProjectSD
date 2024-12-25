using Xunit;
using ProjectManagementApp;

public class UserTests
{
    [Fact]
    public void Constructor_ShouldInitializeUsername()
    {
        string username = "testuser";

        // Since User is an abstract class, we need a concrete subclass (MockUser) 
        // to test its shared functionality. This ensures we can verify behaviors like 
        // username initialization that are common across all subclasses.
        var user = new MockUser(username, "password123", "Grace", "O'Mahony", "gracy@mail.com", "1309130913");

        Assert.Equal(username, user.Username);
    }

    [Fact]
    public void SetHashSalt_ShouldUpdatePasswordHashAndSalt()
    {
        var user = new MockUser("testuser", "password123", "Fred", "Smith", "f.smith@yahoo.com", "0810810812");
        string passwordHash = "hashedpassword";
        byte[] passwordSalt = new byte[] { 1, 2, 3, 4, 5 };

        // Verifies that SetHashSalt properly updates the password hash and salt,
        // which are private fields managed by the User base class.
        user.SetHashSalt(passwordHash, passwordSalt);

        Assert.Equal(passwordHash, user.PasswordHash);
        Assert.Equal(passwordSalt, user.PasswordSalt);
    }

    // MockUser is a simple subclass used solely for testing the abstract User class.
    // It provides a way to instantiate User without requiring real-world subclasses
    // like Manager or TeamMember for generic tests.
    public class MockUser : User
    {
        public MockUser(string username, string password, string firstName, string lastName, string email, string phone) : base(username, password, firstName, lastName, email, phone) { }
    }
}