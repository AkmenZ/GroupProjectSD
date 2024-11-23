using System.Collections.Generic;

namespace ProjectManagementApp
{
    public interface IUsersService
    {
        bool AddUser(User user);
        bool DeleteUser(string username);
        bool ChangePassword(string username, string newPassword);
        User GetUserByUsername(string username);
        User GetUserForLogin(string username);
        User GetManagerByUsername(string username);
        IReadOnlyList<User> GetUsersByRole(UserRole role);
        IReadOnlyList<User> GetAllUsers();
    }
}