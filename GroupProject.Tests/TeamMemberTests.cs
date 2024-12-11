using Xunit;
using ProjectManagementApp;

public class TeamMemberTests
{
    [Fact]
    public void Constructor_ShouldSetRoleToTeamMember()
    {
        string username = "testteammember";
        string firstName = "Test";
        string lastName = "Member";
        string email = "testmember@example.com";
        string phoneNumber = "1234567890";

        // The constructor should assign the correct role to a TeamMember instance.
        var teamMember = new TeamMember(username, "password123", firstName, lastName, email, phoneNumber);

        Assert.Equal(UserRole.TeamMember, teamMember.Role);
    }

    [Fact]
    public void SetHashSalt_ShouldUpdatePasswordHashAndSalt()
    {
        string username = "testteammember";
        string firstName = "Test";
        string lastName = "Member";
        string email = "testmember@example.com";
        string phoneNumber = "1234567890";

        var teamMember = new TeamMember(username, "password123", firstName, lastName, email, phoneNumber);
        string passwordHash = "hashedpassword";
        byte[] passwordSalt = new byte[] { 1, 2, 3, 4, 5 };

        // Ensure the SetHashSalt method correctly updates hash and salt.
        teamMember.SetHashSalt(passwordHash, passwordSalt);

        Assert.Equal(passwordHash, teamMember.PasswordHash);
        Assert.Equal(passwordSalt, teamMember.PasswordSalt);
    }
}