using System;

namespace ProjectManagementApp
{
    public class AuthService : IAuthService
    {
        //Property to store IPasswordService dependency
        private readonly IPasswordService _passwordService;
        //Property to check if a user is logged in, must be false by default
        public bool IsUserLoggedIn { get; private set; } = false;
        //Property to store the current logged in username
        public string CurrentUsername { get; private set; }
        //Property to store the current logged in user role
        public UserRole CurrentUserRole { get; private set; } = UserRole.System;
        //Property to store the UsersManager dependency
        private IUsersService _usersService;

        //Method to set the UsersManager dependency,
        //Can't inject through constructor as it is initialized after SessionManager

        //Parameterless constructor
        public AuthService() { }
        //Constructor to inject the IPasswordService dependency
        public AuthService(IPasswordService passwordService)
        {
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(IPasswordService));
        }

        public void SetUsersService(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(IUsersService));
        }

        //Login
        public bool Login(string username, string password)
        {
            try
            {
                //Get user by username
                User user = _usersService.GetUserForLogin(username);
                //Check if user exists and password is correct
                if (user == null || !_passwordService.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                {
                    //The message should not specify if the username or password is incorrect
                    //due to security reasons
                    throw new Exception("Invalid username or password");
                }

                //Set session properties
                CurrentUsername = user.Username;
                CurrentUserRole = user.Role;
                IsUserLoggedIn = true;
                //Return true when login is successful
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Login Error: {ex.Message}");
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
