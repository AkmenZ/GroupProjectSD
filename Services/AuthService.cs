using System;

namespace ProjectManagementApp
{
    public class AuthService
    {
        //Property to check if a user is logged in, must be false by default
        public bool IsUserLoggedIn { get; private set; } = false;
        //Property to store the current logged in username
        public string CurrentUsername { get; private set; }
        //Property to store the current logged in user role
        public UserRole CurrentUserRole { get; private set; }
        //Property to store the UsersManager dependency
        private UsersService _usersService;

        //Method to set the UsersManager dependency,
        //Can't inject through constructor as it is initialized after SessionManager
        public void SetUsersService(UsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        //Login
        public bool Login(string username, string password)
        {
            try
            {
                //Get user by username
                User user = _usersService.GetUserByUsername(username);
                //Check if user exists and password is correct
                if (user == null || !user.VerifyPassword(password))
                {
                    //The message should not specify if the username or password is incorrect
                    //due to security reasons
                    throw new Exception("\n  Invalid username or password");
                }

                //Set session properties
                CurrentUsername = username;
                CurrentUserRole = user.Role;
                IsUserLoggedIn = true;
                //Return true when login is successful
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Error: {ex.Message}");
                return false;
            }
        }

        //Logout
        public void Logout()
        {
            //Reset session properties
            CurrentUsername = null;
            CurrentUserRole = default(UserRole);
            IsUserLoggedIn = false;
        }
    }
}
