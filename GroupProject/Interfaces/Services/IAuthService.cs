using System;

namespace ProjectManagementApp
{
    public interface IAuthService
    {
        bool IsUserLoggedIn { get; }
        string CurrentUsername { get; }
        UserRole CurrentUserRole { get; }
        void SetUsersService(IUsersService usersService);
        bool Login(string username, string password);
        void Logout();
    }
}