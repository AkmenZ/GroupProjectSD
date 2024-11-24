using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp
{
    public class UsersService : IUsersService
    {
        //List to store users
        private List<User> _users;
        //Property to store user repository
        private readonly IRepository<User> _userRepository;
        //Properties to store dependencies        
        private readonly IPasswordService _passwordService;
        private readonly ILoggerService _loggerService;

        //Constructor, dependency injection
        public UsersService(IRepository<User> userRepository, IPasswordService passwordService, ILoggerService loggerService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _loggerService = loggerService;           
            _users = _userRepository.GetAll();
        }

        //Add user
        public bool AddUser(User user, string password)
        {
            try
            {
                //Get data
                _users = _userRepository.GetAll();
                //Check if username already exists
                if (_users.Any(u => u.Username == user.Username))
                {
                    throw new Exception($"Username: {user.Username} already exists.");
                }
                //Generate password hash and salt
                Byte[] passwordSalt = _passwordService.GenerateSalt();
                string passwordHash = _passwordService.HashPassword(password, passwordSalt);
                user.SetHashSalt(passwordHash, passwordSalt);
                //Add user
                _users.Add(user);
                //Save changes
                _userRepository.SaveAll(_users);
                //Log action
                _loggerService.LogAction($"User added: {user.Username}");
                //Return true when added
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Add User: {ex.Message}");
            }
        }
        public bool DeleteUser(string username)
        {
            try
            {
                //Get data
                _users = _userRepository.GetAll();
                //Get user
                var user = GetUserByUsername(username);
                //Delete user
                _users.Remove(user);
                //Save changes
                _userRepository.SaveAll(_users);
                //Log action
                _loggerService.LogAction($"User deleted: {username}");
                //Return true when deleted
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Delete User: {ex.Message}");
            }
        }

        public bool ChangePassword(string username, string newPassword)
        {
            try
            {
                //Get data
                _users = _userRepository.GetAll();
                //Get user
                var user = GetUserByUsername(username);
                //Generate new password hash and salt
                var passwordSalt = _passwordService.GenerateSalt();
                var passwordHash = _passwordService.HashPassword(newPassword, passwordSalt);
                //Change password
                user.SetHashSalt(passwordHash, passwordSalt);
                //Save changes
                _userRepository.SaveAll(_users);
                //Log action
                _loggerService.LogAction($"Password changed for: {username}");
                //Return true when password changed
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Change Password: {ex.Message}");
            }
        }

        //Get user by username
        public User GetUserByUsername(string username)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    throw new Exception($"User with username {username} not found.");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error get user by username: {ex.Message}");
            }
        }

        //Retrieve user for login. Return null if not found instead of throwing exception due to security reasons
        public User GetUserForLogin(string username)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error get user by username: {ex.Message}");
            }
        }

        //Get manager by username
        public User GetManagerByUsername(string username)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Role == UserRole.Manager && u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    throw new Exception($"Manager with username {username} not found.");
                }                
                
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error get Manager by username: {ex.Message}");
            }
        }

        //Get users by role, return as read-only list
        public IReadOnlyList<User> GetUsersByRole(UserRole role)
        {
            try
            {
                return _users.Where(u => u.Role == role).ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error get users by role: {ex.Message}");
            }
        }

        //Get all users, return as read-only list
        public IReadOnlyList<User> GetAllUsers()
        {
            try
            {
                return _users.OrderBy(u => u.Role).ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error get all users: {ex.Message}");
            }
        }
    }
}