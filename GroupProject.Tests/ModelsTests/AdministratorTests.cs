using Xunit;
using ProjectManagementApp;
using Newtonsoft.Json;
using System.Linq;

public class AdministratorTests
{
    [Fact]
    public void Constructor_ShouldSetRoleToAdministrator()
    {
        string username = "testadmin";
        string password = "password123";
        string firstName = "John";
        string lastName = "Doe";
        string email = "johndoe@mail.com";
        string phone = "123456789";

        // Ensure the constructor correctly assigns the Administrator role.
        var admin = new Administrator(username, password, firstName, lastName, email, phone);

        Assert.Equal(UserRole.Administrator, admin.Role);
    }

    [Fact]
    public void JsonConstructor_ShouldDeserializeCorrectly()
    {
        // Correct JSON structure with Role = 3 (Administrator).
        string json = "{\"Username\":\"testadmin\",\"PasswordHash\":\"hashedpassword\",\"PasswordSalt\":[1,2,3,4,5],\"Role\":3}";

        // Deserialize the JSON string into an Administrator object.
        var admin = JsonConvert.DeserializeObject<Administrator>(json);

        // Assert that all properties were deserialized correctly.
        Assert.Equal("testadmin", admin.Username);
        Assert.Equal("hashedpassword", admin.PasswordHash);

        // Use SequenceEqual for array comparison.
        Assert.True(admin.PasswordSalt.SequenceEqual(new byte[] { 1, 2, 3, 4, 5 }));

        // Verify that the role is deserialized as Administrator.
        Assert.Equal(UserRole.Administrator, admin.Role);
    }
}